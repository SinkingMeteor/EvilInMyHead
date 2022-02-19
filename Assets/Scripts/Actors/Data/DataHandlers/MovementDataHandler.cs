using System;
using System.Collections.Generic;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors.Data
{
    public class MovementDataHandler : SerializedMonoBehaviour, IActorMovementDataProvider, IEffectHandler
    {
        public event Action<ActorEffectType> OnEffectExpired;
        public float Speed => _influencingEffects.Last.Value.GetMovementData(FormDataPackage()).Speed;
        
        [OdinSerialize] private ActorMovementConfig initialDataProvider;

        private LinkedList<IMovementEffect> _influencingEffects;
        private MovementDataPackage _movementDataPackage;
        private ActorsEffectFactory _effectFactory;

        public void Initialize(ActorsEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _influencingEffects = new LinkedList<IMovementEffect>();
            _movementDataPackage = new MovementDataPackage();
            _influencingEffects.AddLast(new DefaultMovementGetter());
        }
        public void AddEffect(ActorEffectType effectType)
        {
            var effect = _effectFactory.MovementEffectsFactory.GetEffect(effectType);
            effect.Setup();
            _influencingEffects.AddLast(effect);
            _influencingEffects.Last.Value.SetWrapper(_influencingEffects.Last.Previous.Value);
        }

        public void Tick()
        {
            var currentEffect = _influencingEffects.First;
            for (int i = 0; i < _influencingEffects.Count; i++)
            {
                currentEffect.Value.Tick();
                if (currentEffect.Value.IsExpired)
                {
                    if (currentEffect.Next != null)
                        currentEffect.Next.Value.SetWrapper(currentEffect.Previous.Value);
                    _influencingEffects.Remove(currentEffect);
                    OnEffectExpired?.Invoke(currentEffect.Value.Config.EffectType);
                    i -= 1;
                }
                currentEffect = currentEffect.Next;
            }
        }
        private MovementDataPackage FormDataPackage()
        {
            _movementDataPackage.Speed = initialDataProvider.Speed;
            return _movementDataPackage;
        }
    }
}
