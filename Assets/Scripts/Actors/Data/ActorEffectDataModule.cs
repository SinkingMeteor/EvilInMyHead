using System;
using System.Collections.Generic;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public class ActorEffectDataModule
    {
        public event Action<ActorEffectType, float> OnEffectAdded;
        
        public IReadOnlyList<ActorEffectType> Effects => _currentEffects;
        private List<ActorEffectType> _currentEffects;

        public ActorEffectDataModule()
        {
            _currentEffects = new List<ActorEffectType>();
        }

        public bool Contains(ActorEffectType effectType) => _currentEffects.Contains(effectType);

        public void AddEffect(ActorEffectType effectType, float duration)
        {
            if (Contains(effectType)) return;
            _currentEffects.Add(effectType);
            OnEffectAdded?.Invoke(effectType, duration);
        }

        public void RemoveEffect(ActorEffectType effectType)
        {
            _currentEffects.Remove(effectType);
        }
    }
}