using System;
using System.Collections.Generic;
using Sheldier.Gameplay.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    public class MovementDataHandler : SerializedMonoBehaviour, IActorMovementDataProvider, ISubDataHandler
    {
        public float Speed => _movementEffects.Last.Value.GetMovementData(FormDataPackage()).Speed;
        
        [OdinSerialize] private IActorMovementDataProvider initialDataProvider;

        private MovementDataPackage _movementDataPackage;
        private LinkedList<IMovementEffect> _movementEffects;

        public void Initialize()
        {
            _movementDataPackage = new MovementDataPackage();
            _movementEffects = new LinkedList<IMovementEffect>();
            _movementEffects.AddLast(new DefaultMovementGetter());
        }

        public void Tick()
        {
            var currentNode = _movementEffects.First;
            for (int i = 0; i < _movementEffects.Count; i++)
            {
                if (currentNode.Value.IsExpired)
                {
                    Debug.Log($"Effect {currentNode.Value.GetType().Name} expired!");
                    _movementEffects.Remove(currentNode);
                    i -= 1;
                }
                currentNode = currentNode.Next;
            }
        }
        public void AddEffect(IEffect newEffect)
        {
            if (newEffect is not IMovementEffect effect)
                return;

            _movementEffects.AddLast(effect);
            if (_movementEffects.Last.Previous == null)
                throw new NullReferenceException(
                    $"New effect in handler {gameObject.name} is alone in list. Base effect wasn't added");
            _movementEffects.Last.Value.SetWrapper(_movementEffects.Last.Previous.Value);
        }

        private MovementDataPackage FormDataPackage()
        {
            _movementDataPackage.Speed = initialDataProvider.Speed;
            return _movementDataPackage;
        }
    }
}
