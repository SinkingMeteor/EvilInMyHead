using Sheldier.Gameplay.Effects;
using Zenject;

namespace Sheldier.Factories
{
    public class ActorsEffectFactory
    {
        public MovementEffectsFactory MovementEffectsFactory => _movementEffectsFactory;
        

        private EffectsDataMap _effectsDataMap;
        private MovementEffectsFactory _movementEffectsFactory;
        public void Initialize()
        {
            _movementEffectsFactory = new MovementEffectsFactory(_effectsDataMap);
        }

        [Inject]
        private void InjectDependencies(EffectsDataMap effectsDataMap)
        {
            _effectsDataMap = effectsDataMap;
        }

        public ActorEffectGroup GetEffectGroup(ActorEffectType effectConfig)
        {
            return _effectsDataMap.EffectMap[effectConfig].EffectGroup;
        }
    }
}
