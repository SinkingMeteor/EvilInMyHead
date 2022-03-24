using Sheldier.Common.Animation;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorJumpState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public bool TransitionConditionIsDone => _isJumping;
        public int Priority => 4;

        private bool _isLocked;
        private bool _isJumping;
        private ActorInputController _inputController;
        private AnimationType[] _animationHashes;
        private Rigidbody2D _actorsRigidbody;
        private ActorTransformHandler _actorTransformHandler;
        private ActorsView _actorsView;

        public void Initialize()
        {
            _inputController.OnJumpButtonPressed += JumpButtonPressed;
            InitializeHashes();
        }

        private void JumpButtonPressed() => _isJumping = true;

        private void InitializeHashes()
        {
            _animationHashes = new AnimationType[]
            {
                AnimationType.Jump_Front,
                AnimationType.Jump_Front_Side,
                AnimationType.Jump_Back_Side,
                AnimationType.Jump_Back
            };
        }

        public void SetDependencies(ActorInternalData data)
        {
            _inputController = data.Actor.InputController;
            _actorsRigidbody = data.Rigidbody2D;
            _actorsView = data.Actor.ActorsView;
            _actorTransformHandler = data.ActorTransformHandler;
        }

        public void Enter()
        {
            _actorsRigidbody.AddForce(_inputController.MovementDirection * 135.0f);
            ActorDirectionView directionView = GetDirectionView();
            SetNewAnimation(_animationHashes[(int)directionView]);
            _actorsView.Animator.OnAnimationEnd += OnJumpingEnd;
        }

        private void OnJumpingEnd()
        {
            _actorsRigidbody.velocity = Vector2.zero;
            _isJumping = false;
        }

        public void Exit()
        {
            _actorsView.Animator.OnAnimationEnd -= OnJumpingEnd;
        }

        public void Tick()
        {

        }

        public void FixedTick()
        {
            
        }

        public void Dispose()
        {
            _inputController.OnJumpButtonPressed -= JumpButtonPressed;
        }
        private ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateMovementDirection();
        private void SetNewAnimation(AnimationType animationID) => _actorsView.PlayAnimation(animationID);

    }
}