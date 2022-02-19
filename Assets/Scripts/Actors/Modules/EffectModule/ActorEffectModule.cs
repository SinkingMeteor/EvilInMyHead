using System;
using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors
{
    public class ActorEffectModule : SerializedMonoBehaviour, IActorEffectModule
    {
        public IReadOnlyList<ActorEffectType> EffectCollection => _effectsCollection;

        [OdinSerialize] private Dictionary<ActorEffectGroup, IEffectHandler> _effectHandlers;
        
        private List<ActorEffectType> _effectsCollection;
        private ActorsEffectFactory _factory;

        public void Initialize(ActorsEffectFactory factory)
        {
            _factory = factory;
            _effectsCollection = new List<ActorEffectType>();
            foreach (var effectHandler in _effectHandlers)
            {
                effectHandler.Value.Initialize(factory);
                effectHandler.Value.OnEffectExpired += RemoveEffect;
            }
        }

        private void RemoveEffect(ActorEffectType effect)
        {
            _effectsCollection.Remove(effect);
        }

        public bool IsEffectExists(ActorEffectType effectType) => _effectsCollection.Contains(effectType);
        public void AddEffect(ActorEffectType effectType)
        {
            var group = _factory.GetEffectGroup(effectType);
            if (IsEffectExists(effectType) || !_effectHandlers.ContainsKey(group))
                return;
            _effectsCollection.Add(effectType);
            _effectHandlers[group].AddEffect(effectType);
        }

        public void Tick()
        {
            foreach (var effectHandler in _effectHandlers)
            {
                effectHandler.Value.Tick();
            }
        }
        private void OnDestroy()
        {
            #if UNITY_EDITOR
                if (!GameGlobalSettings.IsStarted) return;
            #endif
            foreach (var effectHandler in _effectHandlers)
            {
                effectHandler.Value.OnEffectExpired -= RemoveEffect;
            }
        }
    }
}
