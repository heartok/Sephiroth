using System;
using System.Collections.Generic;
using Ploeh.AutoFixture;
using Xunit;

namespace Sephiroth.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GivenAPlayer_WhenTheirHealthIsLow_ThenTheyShouldRest()
        {
            // Fixture setup
            var fixture = new Fixture();
            var sut = fixture.Customize(new InjuredPlayerCustomization()).Create<Player>();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<RestDecision>(result);
            // Teardown
        }

        [Fact]
        public void GivenAPlayer_WhenTheirHealthIsNotLow_NoDecisionIsMade()
        {
            // Fixture setup
            var fixture = new Fixture();
            var sut = fixture.Customize(new HealthyPlayerCustomization()).Create<Player>();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<NoDecision>(result);
            // Teardown
        }

        [Fact]
        public void GivenAPlayer_WhenPlayerIsInjuredWithNoRestDecision_ReturnsNoDecision()
        {
            // Fixture setup
            var fixture = new Fixture();
            var sut = fixture.Customize(new InjuredPlayerCustomization()).Create<Player>();
            sut.Decisions.Clear();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<NoDecision>(result);
            // Teardown
        }
    }

    public class HealthyPlayerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Player>(composer => composer
                .Without(p => p.Decisions)
                .With(p => p.LowHealth, 25)
                .With(p => p.HighHealth, 75)
                .With(p => p.Health, 100));
        }
    }

    public class InjuredPlayerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Player>(composer => composer
                .Without(p => p.Decisions)
                .With(p => p.LowHealth, 25)
                .With(p => p.HighHealth, 75)
                .With(p => p.Health, 50));
        }
    }

    public class NoDecision : IDecision
    {
    }

    public class RestDecision : IDecision
    {
    }

    public class Player
    {
        public int LowHealth { get; set; }
        public int HighHealth { get; set; }
        public int Health { get; set; }
        public HashSet<Type> Decisions { get; set; }

        public Player()
        {
            Decisions = new HashSet<Type> {typeof(RestDecision)};
        }

        public IDecision MakeDecision()
        {
            if (LowHealth < Health && HighHealth > Health)
            {
                if (Decisions.Contains(typeof(RestDecision)))
                {
                    return new RestDecision();
                }                
            }
            return new NoDecision();
        }
    }

    public interface IDecision
    {
    }
}