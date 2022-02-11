using System;
using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    [System.Serializable]
    public class ActorIdleState : IStateComponent
    {
        public event Action<int> OnNewAnimation;
        public bool IsLocked => _isLocked;

        private bool _isLocked = false;
        private ActorStateController _actorStateController;
        
        private int[] _animationHashes;
        private ActorInputController _inputController;
        private ActorTransformHandler _actorTransformHandler;

        public void SetDependencies(ActorStateController actorStateController, ActorInputController inputController, ActorTransformHandler actorTransformHandler)
        {
            _actorTransformHandler = actorTransformHandler;
            _inputController = inputController;
            _actorStateController = actorStateController;

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
            ActorDirectionView directionView = _actorTransformHandler.CurrentDirectionView;
            OnNewAnimation?.Invoke(_animationHashes[(int)directionView]);
            _actorStateController.SetCurrentState(this);
        }
    }
}