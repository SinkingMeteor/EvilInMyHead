using DG.Tweening;
using Sheldier.Actors.Interact;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorMovesObjectsState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public bool TransitionConditionIsDone => _stateData.Get(GameplayConstants.MOVES_OBJECTS_STATE_DATA).StateValue;
        public int Priority => 5;

        private readonly DynamicNumericalEntityStatsCollection _numericalStats;
        
        private bool _isLocked;
        private bool _isEntered;
        private ActorStateDataModule _stateData;
        private ActorInputController _inputController;
        private IInteractReceiver _interactReceiver;
        private ActorNotifyModule _notifier;
        private Rigidbody2D _rigidbody2D;
        private ActorsView _actorsView;
        private Actor _actor;
        private Vector2 _viewDirection;
        private AnimationType _moveAnimation;
        private AnimationType _idleAnimation;


        public ActorMovesObjectsState(DynamicNumericalEntityStatsCollection numericalStats)
        {
            _numericalStats = numericalStats;
        }
        public void Initialize()
        {
            _notifier.OnInteracted += OnInteractedWithMoveObject;
        }

        public void SetDependencies(ActorInternalData data)
        {
            _stateData = data.Actor.StateDataModule;
            _inputController = data.Actor.InputController;
            _notifier = data.Actor.Notifier;
            _actor = data.Actor;
            _rigidbody2D = data.Rigidbody2D;
            _actorsView = data.Actor.ActorsView;
        }

        public async void Enter()
        {
            _stateData.Get(GameplayConstants.DOES_ANY_STATE_DATA).SetState(true);
            var transform = _actor.transform;
            transform.localScale = Vector3.one;
            await transform.DOMove(_interactReceiver.Transform.position, 0.3f).AsyncWaitForCompletion();
            _interactReceiver.Transform.parent.SetParent(transform);
            var direction = (_interactReceiver.Transform.parent.transform.position - transform.position).normalized;
            SetViewDirection(direction);
            _isEntered = true;
        }

        private void OnInteractedWithMoveObject(IInteractReceiver interactReceiver)
        {
            if (interactReceiver.ReceiverType != GameplayConstants.INTERACT_RECEIVER_MOVING_OBJECT)
                return;
            _interactReceiver = interactReceiver;

        }

        private void SetViewDirection(Vector3 direction)
        {
            Debug.Log(direction);
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                bool isRight = direction.x > 0;
                _viewDirection = isRight ? Vector2.right : Vector2.left;
                _moveAnimation = isRight ? AnimationType.Move_Object_Move_Left : AnimationType.Move_Object_Move_Right;
                _idleAnimation = isRight ? AnimationType.Move_Object_Idle_Left : AnimationType.Move_Object_Idle_Right;
            }
            else
            {
                bool isUp = direction.y > 0;
                _viewDirection = isUp ? Vector2.up : Vector2.down;
                _moveAnimation = AnimationType.Move_Object_Move_Front;
                _idleAnimation = AnimationType.Move_Object_Idle_Front;
            }
        }

        public void Exit()
        {
            _stateData.Get(GameplayConstants.DOES_ANY_STATE_DATA).SetState(false);
            _interactReceiver.Transform.parent.SetParent(null);
            _isEntered = false;
        }

        public void Tick()
        {
            if (!_isEntered)
                return;
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            SetNewAnimation(movementDirection.sqrMagnitude < 0.2f ? _idleAnimation : _moveAnimation);
        }

        public void FixedTick()
        {
            if (!_isEntered)
                return;
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            if (movementDirection.sqrMagnitude < 0.2f || Vector2.Dot(movementDirection.normalized, _viewDirection) < 0.5f)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }
            movementDirection = _viewDirection;
            var movementDistance = movementDirection * _numericalStats.Get(StatsConstants.ACTOR_MOVEMENT_SPEED_STAT).BaseValue / 2.0f;
            _rigidbody2D.velocity = movementDistance;
        }
        private void SetNewAnimation(AnimationType animationID) => _actorsView.PlayAnimation(animationID);

        public void Dispose()
        {
            _notifier.OnInteracted -= OnInteractedWithMoveObject;
        }
    }
}