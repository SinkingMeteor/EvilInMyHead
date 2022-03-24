using Sheldier.Common.Animation;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorDefaultIdleState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public virtual bool TransitionConditionIsDone => true;
        public virtual int Priority => 0;

        protected AnimationType[] _animationHashes;
        
        private bool _isLocked = false;
        private ActorTransformHandler _actorTransformHandler;
        private ActorsView _actorsView;

        public void Initialize()
        {
            InitializeHashes();
        }

        public virtual void SetDependencies(ActorInternalData data)
        {
            _actorTransformHandler = data.ActorTransformHandler;
            _actorsView = data.Actor.ActorsView;
        }

        protected virtual void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationType.Idle_Front,
                AnimationType.Idle_Front_Side,
                AnimationType.Idle_Back_Side,
                AnimationType.Idle_Back
            };
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public virtual void Tick()
        {
            ActorDirectionView directionView = _actorTransformHandler.CalculateViewDirection();
            SetNewAnimation(_animationHashes[(int)directionView]);
        }

        public void FixedTick()
        {
            
        }

        public void Dispose()
        {
            
        }

        private void SetNewAnimation(AnimationType animationID) => _actorsView.PlayAnimation(animationID);

    }
}