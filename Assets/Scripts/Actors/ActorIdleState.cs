using System;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorIdleState : MonoBehaviour, IStateComponent
    {
        public event Action<int> OnNewAnimation;
        public bool IsLocked => _isLocked;
        public bool TransitionConditionIsDone => true;
        public int Priority => 0;

        private bool _isLocked = false;
        
        private int[] _animationHashes;
        private ActorInputController _inputController;
        private ActorTransformHandler _actorTransformHandler;

        public void SetDependencies(ActorInputController inputController, ActorTransformHandler actorTransformHandler)
        {
            _actorTransformHandler = actorTransformHandler;
            _inputController = inputController;

            _animationHashes = new[]
            {
                Animator.StringToHash("Idle_Front"),
                Animator.StringToHash("Idle_Front_Side"),
                Animator.StringToHash("Idle_Back_Side"),
                Animator.StringToHash("Idle_Back"),
            };
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            ActorDirectionView directionView = _actorTransformHandler.CalculateViewDirection();
            OnNewAnimation?.Invoke(_animationHashes[(int)directionView]);
        }
    }
}