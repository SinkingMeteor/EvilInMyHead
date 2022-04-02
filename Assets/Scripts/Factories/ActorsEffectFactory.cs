using System.Collections.Generic;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Factories
{
    public class ActorsEffectFactory
    {
        private Dictionary<int, IEffect> _effects;

        public void Initialize()
        {
            _effects = new Dictionary<int, IEffect>
            {
                {0, new FreezeMovementEffect(0)}
            };
        }
        
        public IEffect GetEffect(int effectID)
        {
            return _effects[effectID].Clone();
        }
    }
}
