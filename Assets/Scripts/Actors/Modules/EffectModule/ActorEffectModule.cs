using System;
using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Zenject;

namespace Sheldier.Actors
{
    public class ActorEffectModule : SerializedMonoBehaviour, IActorEffectModule
    {
        public IReadOnlyList<ActorEffectType> EffectCollection => _effectsCollection;

        [OdinSerialize] private Dictionary<ActorEffectGroup, IEffectHandler> _effectHandlers;
        
        private List<ActorEffectType> _effectsCollection;
        private ActorsEffectFactory _factory;

        public void Initialize()
        {
            _effectsCollection = new List<ActorEffectType>();
            foreach (var effectHandler in _effectHandlers)
            {
                effectHandler.Value.Initialize(_factory);
                effectHandler.Value.OnEffectExpired += RemoveEffect;
            }
        }

        [Inject]
        private void InjectDependencies(ActorsEffectFactory effectFactory)
        {
            _factory = effectFactory;
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
        public void Dispose()
        {
            foreach (var effectHandler in _effectHandlers)
            {
                effectHandler.Value.OnEffectExpired -= RemoveEffect;
            }
        }
    }
}
