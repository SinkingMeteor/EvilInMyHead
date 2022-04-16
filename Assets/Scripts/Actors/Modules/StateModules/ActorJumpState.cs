using System.Collections;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Utilities;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorJumpState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public bool TransitionConditionIsDone => _data.Get(GameplayConstants.JUMP_STATE_DATA).StateValue;
        public int Priority => 4;

        private bool _isLocked;
        private bool _isLanded;
        private ActorInputController _inputController;
        private AnimationType[] _animationHashes;
        private Rigidbody2D _actorsRigidbody;
        private ActorTransformHandler _actorTransformHandler;
        private ActorsView _actorsView;
        private Actor _actor;
        private ActorDirectionView _directionView;
        private ActorStateDataModule _data;
        private TickHandler _tickHandler;

        public void Initialize()
        {
            _inputController.OnJumpButtonPressed += JumpButtonPressed;
            InitializeHashes();
        }

        private void JumpButtonPressed()
        {
            if(_inputController.MovementDirection != Vector2.zero)
                _data.Get(GameplayConstants.JUMP_STATE_DATA).SetState(true);
        }

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
            _actor = data.Actor;
            _data = _actor.StateDataModule;
            _inputController = data.Actor.InputController;
            _actorsRigidbody = data.Rigidbody2D;
            _actorsView = data.Actor.ActorsView;
            _tickHandler = data.TickHandler;
            _actorTransformHandler = data.ActorTransformHandler;
        }

        public void Enter()
        {
            _isLanded = false;
            _actorsRigidbody.AddForce(_inputController.MovementDirection * 135.0f);
            _directionView = GetDirectionView();
            SetNewAnimation(_animationHashes[(int)_directionView]);
            _actorsView.Animator.OnAnimationTriggered += OnLanded;
            _actorsView.Animator.OnAnimationEnd += OnJumpingEnd;
            _data.Get(GameplayConstants.DOES_ANY_STATE_DATA).SetState(true);
        }

        private void OnLanded()
        {
            bool inPit = Physics2D.OverlapCircle(_actor.transform.position.DiscardZ(), 0.17f, EnvironmentConstants.PIT_LAYER_MASK);
            if (!inPit)
            {
                _isLanded = true;
                return;
            }

            _actor.Notifier.NotifyFalling(_directionView);
            OnJumpingEnd();                    
        }

        private void OnJumpingEnd()
        {
            _data.Get(GameplayConstants.JUMP_STATE_DATA).SetState(false);
            _isLanded = false;
        }

        public void Exit()
        {
            _actorsRigidbody.velocity = Vector2.zero;
            _actorsView.Animator.OnAnimationTriggered -= OnLanded;
            _actorsView.Animator.OnAnimationEnd -= OnJumpingEnd;
            _data.Get(GameplayConstants.DOES_ANY_STATE_DATA).SetState(false);
        }

        public void Tick()
        {

        }

        public void FixedTick()
        {
            if (_isLanded)
                _actorsRigidbody.velocity = Vector2.Lerp(_actorsRigidbody.velocity, Vector2.zero, _tickHandler.TickDelta * 10.0f);
        }

        public void Dispose()
        {
            _inputController.OnJumpButtonPressed -= JumpButtonPressed;
        }
        private ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateMovementDirection();
        private void SetNewAnimation(AnimationType animationID) => _actorsView.PlayAnimation(animationID);

    }
}