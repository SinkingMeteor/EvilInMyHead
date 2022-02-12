using System;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorControlledMovementState : MonoBehaviour, IStateComponent
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        
        private ActorInputController _inputController;
        private ActorTransformHandler _actorTransformHandler;
        public event Action<int> OnNewAnimation;
        public bool IsLocked => _isLocked;
        public bool TransitionConditionIsDone => _inputController.CurrentInputProvider.MovementDirection.sqrMagnitude > 0;
        public int Priority => 1;

        private bool _isLocked;
        private int[] _animationHashes;
        
        public void SetDependencies(ActorInputController inputController,
            ActorTransformHandler actorTransformHandler)
        {
            _inputController = inputController;
            _actorTransformHandler = actorTransformHandler;
            
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
            Debug.Log("Exit");
            rigidbody2D.velocity = Vector2.zero;
        }

        public void Tick()
        {
            ActorDirectionView directionView = _actorTransformHandler.CalculateMovementDirection();
            OnNewAnimation?.Invoke(_animationHashes[(int)directionView]);
            
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            var movementDistance = movementDirection * 2.0f;
            rigidbody2D.velocity = movementDistance;
        }
    }
}