using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Gameplay.Effects;
using Zenject;

namespace Sheldier.Factories
{
    public class ActorsEffectFactory

    {
    private Dictionary<ActorEffectType, IEffect> _effectsCollection;

    private EffectsDataMap _effectsDataMap;

    public void Initialize()
    {
        _effectsCollection = new Dictionary<ActorEffectType, IEffect>
        {
            {ActorEffectType.Freeze, new FreezeMovementEffect(_effectsDataMap.EffectMap[ActorEffectType.Freeze])}
        };
    }

    [Inject]
    private void InjectDependencies(EffectsDataMap effectsDataMap)
    {
        _effectsDataMap = effectsDataMap;
    }

    public IEffect CreateEffect(ActorEffectType effectType)
    {
        return _effectsCollection[effectType].Clone();
    }
    }

}
