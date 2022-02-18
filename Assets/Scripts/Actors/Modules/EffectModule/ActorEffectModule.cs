using System;
using System.Collections.Generic;
using Sheldier.Gameplay.Effects;
using Sirenix.OdinInspector;

namespace Sheldier.Actors
{
    public class ActorEffectModule : SerializedMonoBehaviour, IActorEffectModule
    {
        public event Action<IEffect> OnEffectModuleAddedEffect;
        public event Action<IEffect> OnEffectModuleRemovedEffect;

        public IReadOnlyList<IEffect> EffectCollection => _effectsCollection;

        private List<IEffect> _effectsCollection;
        public bool IsEffectExists(IEffect effect) => _effectsCollection.Contains(effect);
        public bool IsEffectExists(ActorEffectType effectType)
        {
            foreach (var effect in _effectsCollection)
            {
                if (effect.Config.EffectType == effectType)
                    return true;
            }

            return false;
        }

        public void Initialize()
        {
            _effectsCollection = new List<IEffect>();
        }

        public void Tick()
        {
            for (int i = 0; i < _effectsCollection.Count; i++)
            {
                _effectsCollection[i].Tick();
                if (_effectsCollection[i].IsExpired)
                {
                    var lastIndex = _effectsCollection.Count - 1;
                    (_effectsCollection[i], _effectsCollection[lastIndex]) =
                        (_effectsCollection[lastIndex], _effectsCollection[i]);
                    OnEffectModuleRemovedEffect?.Invoke(_effectsCollection[lastIndex]);
                    _effectsCollection.RemoveAt(lastIndex);
                    i -= 1;
                }
            }
        }
        public void AddEffect(IEffect effect)
        {
            if (IsEffectExists(effect))
                return;
            _effectsCollection.Add(effect);
            effect.Setup();
            OnEffectModuleAddedEffect?.Invoke(effect);
        }
    }
}
