using System;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorDefaultIdleState : MonoBehaviour, IStateComponent
    {
        public bool IsLocked => _isLocked;
        public virtual bool TransitionConditionIsDone => true;
        public virtual int Priority => 0;
        
        [SerializeField] private Animator animator;

        protected int[] _animationHashes;
        
        private bool _isLocked = false;
        private ActorInputController _inputController;
        private ActorTransformHandler _actorTransformHandler;

        public void SetDependencies(ActorInputController inputController, ActorTransformHandler actorTransformHandler)
        {
            _actorTransformHandler = actorTransformHandler;
            _inputController = inputController;

            InitializeHashes();
        }

        protected virtual void InitializeHashes()
        {
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
            SetNewAnimation(_animationHashes[(int)directionView]);
        }
        
        private void SetNewAnimation(int animationID) => animator.Play(animationID);

    }
}