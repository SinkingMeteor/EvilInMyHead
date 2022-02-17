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
        
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private Animator animator;
        [OdinSerialize] private IActorMovementDataProvider data;

        protected ActorInputController _inputController;
        private ActorTransformHandler _actorTransformHandler;

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
            rigidbody2D.velocity = Vector2.zero;
        }

        public void Tick()
        {
            ActorDirectionView directionView = _actorTransformHandler.CalculateViewDirection();
            SetNewAnimation(_animationHashes[(int)directionView]);
            
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            var movementDistance = movementDirection * data.Speed;
            rigidbody2D.velocity = movementDistance;
        }
        private void SetNewAnimation(int animationID) => animator.Play(animationID);

    }
}