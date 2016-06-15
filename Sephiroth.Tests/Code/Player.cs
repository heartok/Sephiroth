using System;
using System.Collections.Generic;

namespace Sephiroth.Tests.Code
{
    public class Player
    {
        public int LowHealth { get; set; }
        public int HighHealth { get; set; }
        public int Health { get; set; }
        public HashSet<Type> Decisions { get; set; }
        public HashSet<StatusEffect> StatusEffect { get; set; }

        public Player()
        {
            StatusEffect = new HashSet<StatusEffect>();
            Decisions = new HashSet<Type> {typeof(RestDecision)};
        }

        public IDecision MakeDecision()
        {
            IDecision decision = new NoDecision();

            if (LowHealth < Health && HighHealth > Health)
            {
                if (!StatusEffect.Contains(Code.StatusEffect.Dia))
                {
                    if (Decisions.Contains(typeof(RestDecision)))
                    {
                        decision = new RestDecision();
                    }
                }                
            }

            return decision;
        }
    }
}