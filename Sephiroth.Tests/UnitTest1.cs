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
            var sut = fixture.Build<Player>()
                .With(p => p.LowHealth, 25)
                .With(p => p.HighHealth, 75)
                .With(p => p.Health, 50)
                .Create();
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
            var sut = fixture.Build<Player>()
                .With(p => p.LowHealth, 25)
                .With(p => p.HighHealth, 75)
                .With(p => p.Health, 100)
                .Create();
            // Exercise system
            var result = sut.MakeDecision();
            // Verify outcome
            Assert.IsType<NoDecision>(result);
            // Teardown
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

        public IDecision MakeDecision()
        {
            if(LowHealth < Health && HighHealth > Health) return new RestDecision();
            return new NoDecision();
        }
    }

    public interface IDecision { }
}
