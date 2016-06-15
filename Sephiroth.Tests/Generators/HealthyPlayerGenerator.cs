using System;
using Ploeh.AutoFixture.Kernel;
using Sephiroth.Tests.Code;

namespace Sephiroth.Tests.Generators
{
    public class HealthyPlayerGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var type = request as Type;
            if (type == null) return new NoSpecimen();
            if (type != typeof(Player)) return new NoSpecimen();

            var player = new Player
            {
                LowHealth = 25,
                HighHealth = 75,
                Health = 100
            };

            return player;

        }
    }
}