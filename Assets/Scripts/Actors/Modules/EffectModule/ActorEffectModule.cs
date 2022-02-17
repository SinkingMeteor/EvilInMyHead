using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.Gameplay.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors
{
    public class ActorEffectModule : SerializedMonoBehaviour, IActorModule, IActorEffectModule
    {
        public int Priority => 1;
        
        [OdinSerialize] private IActorsEffectListener[] effectListeners;

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

        public void Initialize(IActorModuleCenter actorModuleCenter)
        {
            _effectsCollection = new List<IEffect>();
        }

        public void Tick()
        {
            for(int i = 0; i < _effectsCollection.Count; i++)
            {
                _effectsCollection[i].Tick();
                if(!_effectsCollection[i].IsExpired)
                    continue;

                var lastIndex = _effectsCollection.Count - 1;
                (_effectsCollection[i], _effectsCollection[lastIndex]) = (_effectsCollection[lastIndex], _effectsCollection[i]);
                _effectsCollection.RemoveAt(lastIndex);
                i -= 1;
            }
        }

        public void AddEffect(IEffect effect)
        {
            if (IsEffectExists(effect))
                return;
            
            _effectsCollection.Add(effect);
            effect.Setup();
            for (int i = 0; i < effectListeners.Length; i++)
                effectListeners[i].OnEffectAdded(effect);
        }
    }
}
