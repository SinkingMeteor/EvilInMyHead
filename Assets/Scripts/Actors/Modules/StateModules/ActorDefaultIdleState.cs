using Sheldier.Common.Animation;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorDefaultIdleState : MonoBehaviour, IStateComponent
    {
        public bool IsLocked => _isLocked;
        public virtual bool TransitionConditionIsDone => true;
        public virtual int Priority => 0;

        protected int[] _animationHashes;
        
        private bool _isLocked = false;
        private ActorTransformHandler _actorTransformHandler;
        private ActorsView _actorsView;

        public virtual void SetDependencies(ActorInternalData data)
        {
            _actorTransformHandler = data.ActorTransformHandler;
            _actorsView = data.Actor.ActorsView;
            InitializeHashes();
        }

        protected virtual void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationConstants.Animations[AnimationType.Idle_Front],
                AnimationConstants.Animations[AnimationType.Idle_Front_Side],
                AnimationConstants.Animations[AnimationType.Idle_Back_Side],
                AnimationConstants.Animations[AnimationType.Idle_Back]
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

        private void SetNewAnimation(int animationID) => _actorsView.PlayAnimation(animationID);

    }
}