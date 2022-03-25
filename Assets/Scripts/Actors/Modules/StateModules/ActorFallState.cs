using System;
using System.Collections;
using Sheldier.Common.Animation;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorFallState : IStateComponent
    {
        public bool IsLocked => _isLocked;
        public bool TransitionConditionIsDone => _data.IsFalling;
        public int Priority => 5;
        
        private bool _isLocked;
        private Actor _actor;
        private ActorTraceProvider _traceProvider;
        private AnimationType[] _animationHashes;
        private ActorDirectionView _directionView;
        private ActorStateDataModule _data;

        public void Initialize()
        {
            _actor.Notifier.OnActorFalls += OnFallSignalReceived;
            InitializeHashes();
        }
        public void SetDependencies(ActorInternalData data)
        {
            _actor = data.Actor;
            _data = _actor.DataModule.StateDataModule;
            _traceProvider = data.ActorTraceProvider;
        }
        
        private void InitializeHashes()
        {
            _animationHashes = new AnimationType[]
            {
                AnimationType.Jump_Fall_Front,
                AnimationType.Jump_Fall_Front_Side,
                AnimationType.Jump_Fall_Back_Side,
                AnimationType.Jump_Fall_Back
            };
        }
        
        private void OnFallSignalReceived(ActorDirectionView view)
        {
            _directionView = view;
            _data.SetFall(true);
        }

        public void Enter()
        {
            _actor.LockInput();
            SetNewAnimation(_animationHashes[(int)_directionView]);
            _actor.ActorsView.Animator.OnAnimationEnd += OnFallingEnd;

        }

        public void Exit()
        {
            _actor.ActorsView.Animator.OnAnimationEnd -= OnFallingEnd;
            _actor.UnlockInput();
        }

        public void Tick()
        {
        }
        public void FixedTick()
        {
        }

        public void Dispose()
        {
            _actor.Notifier.OnActorFalls -= OnFallSignalReceived;
        }
        private void SetNewAnimation(AnimationType animationID) => _actor.ActorsView.PlayAnimation(animationID);
        private void OnFallingEnd()
        {
            _actor.StartCoroutine(FallingCoroutine());
        }

        private IEnumerator FallingCoroutine()
        {
            yield return new WaitForSeconds(1.0f);
            _actor.transform.position = _traceProvider.LastSafePoint;
            _data.SetFall(false);
        }
    }
}