using System.Collections.Generic;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Factories
{
    public class ActorsEffectFactory
    {
        private Dictionary<ActorEffectType, IEffect> _effects;

        public void Initialize()
        {
            _effects = new Dictionary<ActorEffectType, IEffect>
            {
                {ActorEffectType.Freeze, new FreezeMovementEffect()}
            };
        }
        
        public IEffect GetEffect(int effectID)
        {
            return _effects[(ActorEffectType)effectID].Clone();
        }
    }
}
