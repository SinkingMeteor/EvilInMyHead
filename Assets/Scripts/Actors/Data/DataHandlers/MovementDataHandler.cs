using System;
using System.Collections.Generic;
using Sheldier.Gameplay.Effects;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors.Data
{
    public class MovementDataHandler : SerializedMonoBehaviour, IActorMovementDataProvider, IExtraActorModule
    {
        public float Speed => _movementEffects.Last.Value.GetMovementData(FormDataPackage()).Speed;
        
        [OdinSerialize] private ActorMovementConfig initialDataProvider;
        
        private MovementDataPackage _movementDataPackage;
        private LinkedList<IMovementEffect> _movementEffects;
        private IActorModuleCenter _actorModuleCenter;
        private IActorModuleCenter _moduleCenter;
        public int Priority => 0;
        public void Initialize(IActorModuleCenter moduleCenter)
        {
            _moduleCenter = moduleCenter;
            _moduleCenter.ActorEffectModule.OnEffectModuleAddedEffect += OnEffectAdded;
            _moduleCenter.ActorEffectModule.OnEffectModuleRemovedEffect += OnEffectRemoved;
            _movementDataPackage = new MovementDataPackage();
            _movementEffects = new LinkedList<IMovementEffect>();
            _movementEffects.AddLast(new DefaultMovementGetter());
        }

        public void Tick()
        {
        }

        private void OnEffectAdded(IEffect newEffect)
        {
            if (newEffect is not IMovementEffect effect)
                return;

            _movementEffects.AddLast(effect);
            if (_movementEffects.Last.Previous == null)
                throw new NullReferenceException(
                    $"New effect in handler {gameObject.name} is alone in list. Base effect wasn't added");
            _movementEffects.Last.Value.SetWrapper(_movementEffects.Last.Previous.Value);
        }

        private void OnEffectRemoved(IEffect expiredEffect)
        {
            if (expiredEffect is not IMovementEffect effect)
                return;

            if (!_movementEffects.Contains(effect))
                return;
            _movementEffects.Remove(effect);
        }
        private MovementDataPackage FormDataPackage()
        {
            _movementDataPackage.Speed = initialDataProvider.Speed;
            return _movementDataPackage;
        }

        private void OnDestroy()
        {
            if (!GameGlobalSettings.IsStarted) return;
            
            _moduleCenter.ActorEffectModule.OnEffectModuleAddedEffect -= OnEffectAdded;
            _moduleCenter.ActorEffectModule.OnEffectModuleRemovedEffect -= OnEffectRemoved;
        }
    }
}
