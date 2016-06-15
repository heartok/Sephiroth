using System.Collections.Generic;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Dsl;
using Sephiroth.Tests.Code;
using Sephiroth.Tests.Generators;
using Xunit;

namespace Sephiroth.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void MakeDecisionOnLowHealthPlayerReturnsCorrectResult()
        {
            // Fixture setup
            var fixture = new Fixture();
            fixture.Customizations.Add(new InjuredPlayerGenerator());
            var sut = fixture.Create<Player>();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<RestDecision>(result);
            // Teardown
        }

        [Fact]
        public void MakeDecisionOnHealthyPlayerReturnsCorrectResult()
        {
            // Fixture setup
            var fixture = new Fixture();
            fixture.Customizations.Add(new HealthyPlayerGenerator());
            var sut = fixture.Create<Player>();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<NoDecision>(result);
            // Teardown
        }

        [Fact]
        public void MakeDecisionOnInjuredPlayerWithoutRestDecisionReturnsCorrectResult()
        {
            // Fixture setup
            var fixture = new Fixture();
            fixture.Customizations.Add(new InjuredPlayerGenerator());
            var sut = fixture.Create<Player>();
            sut.Decisions.Clear();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<NoDecision>(result);
            // Teardown
        }

        [Fact]
        public void MakeDecisionOnInjuredPlayerWithDiaReturnsCorrectResult()
        {
            // Fixture setup
            var fixture = new Fixture();
            fixture.Customizations.Add(new InjuredPlayerGenerator());
            var sut = fixture.Create<Player>();
            sut.StatusEffect.Add(StatusEffect.Dia);
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<NoDecision>(result);
            // Teardown
        }

        [Fact]
        public void NewPlayerStatusEffectShouldNotBeNull()
        {
            // Fixture setup
            var fixture = new Fixture();
            // Exercise system
            var sut = fixture.Build<Player>()
                .Without(x => x.StatusEffect)
                .Create();
            // Verify outcome
            Assert.NotNull(sut.StatusEffect);
            // Teardown
        }        
    }
}