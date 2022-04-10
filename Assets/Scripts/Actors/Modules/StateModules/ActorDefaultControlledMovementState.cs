using System.Collections;
using Sheldier.Actors.Data;
using Sheldier.Common.Animation;
using Sheldier.Common.Utilities;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorDefaultControlledMovementState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public virtual bool TransitionConditionIsDone => _inputController.CurrentInputProvider.MovementDirection.sqrMagnitude > Mathf.Epsilon;
        public virtual int Priority => 1;

        protected ActorInputController _inputController;
        protected ActorTransformHandler _actorTransformHandler;
        protected AnimationType[] _animationHashes;

        private readonly DynamicNumericalEntityStatsCollection _numericalStats;
        private readonly DynamicStringEntityStatsCollection _stringStats;
        private readonly ActorDynamicConfigData _dynamicConfigData;
        private ActorSoundController _soundController;
        private ActorNotifyModule _notifyModule;
        private Actor _actor;

        private Rigidbody2D _rigidbody2D;
        private ActorsView _actorsView;

        private bool _isLocked;
        private Coroutine _stepCoroutine;

        private const float DELAY = 0.5f;

        public void Initialize()
        {
            InitializeHashes();
        }

        public ActorDefaultControlledMovementState(DynamicNumericalEntityStatsCollection numericalStats, 
                                                   DynamicStringEntityStatsCollection stringStats,
                                                   ActorDynamicConfigData dynamicConfigData)
        {
            _dynamicConfigData = dynamicConfigData;
            _numericalStats = numericalStats;
            _stringStats = stringStats;
        }
        public virtual void SetDependencies(ActorInternalData data)
        {
            _soundController = data.Actor.SoundController;
            _actor = data.Actor;
            _inputController = data.Actor.InputController;
            _notifyModule = data.Actor.Notifier;
            _actorTransformHandler = data.ActorTransformHandler;
            _rigidbody2D = data.Rigidbody2D;
            _actorsView = data.Actor.ActorsView;
        }

        protected virtual void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationType.Run_Front,
                AnimationType.Run_Front_Side,
                AnimationType.Run_Back_Side,
                AnimationType.Run_Back,
            };
        }

        public void Enter()
        {
            _stepCoroutine = _actorsView.StartCoroutine(StepSoundCoroutine());
        }

        public void Exit()
        {
            _actorsView.StopCoroutine(_stepCoroutine);
            _rigidbody2D.velocity = Vector2.zero;
        }

        public virtual void Tick()
        {
            ActorDirectionView directionView = GetDirectionView();
            SetNewAnimation(_animationHashes[(int)directionView]);
            if(Physics2D.OverlapCircle(_actor.transform.position.DiscardZ(), 0.1f, EnvironmentConstants.PIT_LAYER_MASK))
                _notifyModule.NotifyFalling(directionView);
        }

        public void FixedTick()
        {
            var movementDirection = _inputController.CurrentInputProvider.MovementDirection;
            var movementDistance = movementDirection * _numericalStats.Get(StatsConstants.ACTOR_MOVEMENT_SPEED_STAT).BaseValue;
            _rigidbody2D.velocity = movementDistance;
        }

        public void Dispose()
        {
        }

        protected virtual ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateMovementDirection();
        private void SetNewAnimation(AnimationType animationID) => _actorsView.PlayAnimation(animationID);

        private IEnumerator StepSoundCoroutine()
        {
            var stepDelay = new WaitForSeconds(DELAY);
            
            while (true) 
            {
             //   _soundController.PlayAudio(_movementData.StepSound);
                yield return stepDelay;
            }
        }

    }
}