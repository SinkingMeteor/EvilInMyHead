using Sheldier.Actors.Data;
using Sheldier.Common.Animation;

namespace Sheldier.Actors
{
    public class ActorDefaultIdleState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public virtual bool TransitionConditionIsDone => true;
        public virtual int Priority => 0;

        protected AnimationType[] _animationHashes;

        private readonly ActorDynamicConfigData _dynamicConfigData;
        
        private bool _isLocked = false;
        private ActorTransformHandler _actorTransformHandler;
        private ActorsView _actorsView;
        private Actor _actor;

        public ActorDefaultIdleState(ActorDynamicConfigData dynamicConfigData)
        {
            _dynamicConfigData = dynamicConfigData;
        }
        
        
        public void Initialize()
        {
            InitializeHashes();
        }

        public virtual void SetDependencies(ActorInternalData data)
        {
            _actorTransformHandler = data.ActorTransformHandler;
            _actorsView = data.Actor.ActorsView;
            _actor = data.Actor;
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
            _dynamicConfigData.Position = _actor.gameObject.transform.position;
        }

        private void SetNewAnimation(AnimationType animationID) => _actorsView.PlayAnimation(animationID);

    }
}