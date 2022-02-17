using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Gameplay.Effects
{
    [CreateAssetMenu(fileName = "EffectsDataMap", menuName = "Sheldier/Effects/EffectsMap")]
    public class EffectsDataMap : BaseScriptableObject
    {
        public IReadOnlyDictionary<ActorEffectType, EffectConfig> EffectMap => _effectConfigs;

        [OdinSerialize] private Dictionary<ActorEffectType, EffectConfig> _effectConfigs;
    }
}