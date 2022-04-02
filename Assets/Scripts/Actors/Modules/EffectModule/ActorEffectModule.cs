using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public class ActorEffectModule : IExtraActorModule, ITickListener
    {
        private readonly ActorDynamicEffectData _actorDynamicEffectData;
        private readonly ActorsEffectFactory _factory;
        
        private List<IEffect> _influencingEffects;
        private TickHandler _tickHandler;
        private Actor _actor;

        public ActorEffectModule(ActorDynamicEffectData actorDynamicEffectData, ActorsEffectFactory effectFactory)
        {
            _actorDynamicEffectData = actorDynamicEffectData;
            _factory = effectFactory;
        }
        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _tickHandler = data.TickHandler;
            _influencingEffects = new List<IEffect>();

            _actorDynamicEffectData.OnEffectAdded += AddEffect;
            _tickHandler.AddListener(this);
        }

        private void AddEffect(int effectID)
        {
            var effect = _factory.GetEffect(effectID);
            effect.Setup(_actor);
            _influencingEffects.Add(effect);
        }

        public void Tick()
        {
            for (int i = 0; i < _influencingEffects.Count; i++)
            {
                _influencingEffects[i].Tick();
                if (_influencingEffects[i].IsExpired)
                {
                    _actorDynamicEffectData.RemoveEffect(_influencingEffects[i].EffectID);
                    (_influencingEffects[i], _influencingEffects[^1]) =
                        (_influencingEffects[^1], _influencingEffects[i]);
                    _influencingEffects.RemoveAt(_influencingEffects.Count -1);
                    i -= 1;
                }
            }
        }
        public void Dispose()
        {
            _tickHandler.RemoveListener(this);
            _actorDynamicEffectData.OnEffectAdded -= AddEffect;
        }
    }
}
