using System.Collections.Generic;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Factories
{
    public class MovementEffectsFactory
    {
        private readonly EffectsDataMap _effectsDataMap;

        private Dictionary<ActorEffectType, IMovementEffect> _effects;
        public MovementEffectsFactory(EffectsDataMap effectsDataMap)
        {
            _effectsDataMap = effectsDataMap;
            _effects = new Dictionary<ActorEffectType, IMovementEffect>
            {
                {ActorEffectType.Freeze, new FreezeMovementEffect(_effectsDataMap.EffectMap[ActorEffectType.Freeze])}
            };
        }
        
        public IMovementEffect GetEffect(ActorEffectType effectType)
        {
            return _effects[effectType].Clone();
        }
    }
}