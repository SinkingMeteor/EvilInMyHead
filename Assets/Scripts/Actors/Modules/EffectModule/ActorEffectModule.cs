using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors
{
    public class ActorEffectModule : IExtraActorModule, ITickListener
    {

        private List<IEffect> _influencingEffects;
        private ActorsEffectFactory _factory;
        private ActorEffectDataModule _effectData;
        private TickHandler _tickHandler;
        private Actor _actor;

        public ActorEffectModule(ActorsEffectFactory effectFactory)
        {
            _factory = effectFactory;
        }
        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _tickHandler = data.TickHandler;
            _effectData = data.Actor.DataModule.EffectDataModule;
            _influencingEffects = new List<IEffect>();

            _effectData.OnEffectAdded += AddEffect;
            _tickHandler.AddListener(this);
        }

        private void AddEffect(ActorEffectType effectType, float duration)
        {
            var effect = _factory.GetEffect(effectType);
            effect.Setup(_actor, duration);
            _influencingEffects.Add(effect);
        }

        public void Tick()
        {
            for (int i = 0; i < _influencingEffects.Count; i++)
            {
                _influencingEffects[i].Tick();
                if (_influencingEffects[i].IsExpired)
                {
                    _effectData.RemoveEffect(_influencingEffects[i].EffectType);
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
            _effectData.OnEffectAdded -= AddEffect;
        }
    }
}
