using Sheldier.Actors.Data;
using Sheldier.Common.Animation;
using Sheldier.Constants;
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
        
        [OdinSerialize] private IActorMovementDataProvider data;

        protected ActorInputController _inputController;
        protected ActorTransformHandler _actorTransformHandler;
        protected int[] _animationHashes;

        private Rigidbody2D _rigidbody2D;
        private ActorsView _actorsView;

        private bool _isLocked;

        public virtual void SetDependencies(ActorInternalData data)
        {
            _inputController = data.Actor.InputController;
            _actorTransformHandler = data.ActorTransformHandler;
            _rigidbody2D = data.Rigidbody2D;
            _actorsView = data.Actor.ActorsView;
            InitializeHashes();
        }

        protected virtual void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationConstants.Animations[AnimationType.Run_Front],
                AnimationConstants.Animations[AnimationType.Run_Front_Side],
                AnimationConstants.Animations[AnimationType.Run_Back_Side],
                AnimationConstants.Animations[AnimationType.Run_Back],
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
        }

        public void FixedTick()
        {
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            var movementDistance = movementDirection * data.Speed;
            _rigidbody2D.velocity = movementDistance;
        }

        protected virtual ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateMovementDirection();
        private void SetNewAnimation(int animationID) => _actorsView.PlayAnimation(animationID);

    }
}