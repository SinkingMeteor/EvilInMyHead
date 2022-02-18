using System;
using Sheldier.Actors.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorDefaultControlledMovementState : SerializedMonoBehaviour, IStateComponent
    {
        public bool IsLocked => _isLocked;
        public virtual bool TransitionConditionIsDone => _inputController.CurrentInputProvider.MovementDirection.sqrMagnitude > Mathf.Epsilon;
        public virtual int Priority => 1;
        
        [SerializeField]private Rigidbody2D _rigidbody2D;
        [SerializeField]private Animator _animator;
        [OdinSerialize] private IActorMovementDataProvider data;

        protected ActorInputController _inputController;
        protected ActorTransformHandler _actorTransformHandler;

        private bool _isLocked;
        protected int[] _animationHashes;
        
        public void SetDependencies(ActorInputController inputController,
            ActorTransformHandler actorTransformHandler)
        {
            _inputController = inputController;
            _actorTransformHandler = actorTransformHandler;
            InitializeHashes();
        }

        protected virtual void InitializeHashes()
        {
            _animationHashes = new[]
            {
                Animator.StringToHash("Run_Front"),
                Animator.StringToHash("Run_Front_Side"),
                Animator.StringToHash("Run_Back_Side"),
                Animator.StringToHash("Run_Back"),
            };
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        public virtual void Tick()
        {
            ActorDirectionView directionView = GetDirectionView();
            SetNewAnimation(_animationHashes[(int)directionView]);
            
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            var movementDistance = movementDirection * data.Speed;
            _rigidbody2D.velocity = movementDistance;
        }

        protected virtual ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateMovementDirection();
        private void SetNewAnimation(int animationID) => _animator.Play(animationID);

    }
}