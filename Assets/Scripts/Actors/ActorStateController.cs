using System.Collections.Generic;
using Sheldier.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorStateController : SerializedMonoBehaviour
    {
        [OdinSerialize] private List<IStateComponent> states;
        [SerializeField] private Animator animator;
        
        private TickHandler _tickHandler;

        private IStateComponent _previousState;
        private IStateComponent _currentState;


        public bool IsCurrentState(IStateComponent component) => component == _currentState;
        public void SetDependencies(ActorInputController actorInputController, ActorTransformHandler actorTransformHandler)
        {
            foreach (var state in states)
            {
                state.SetDependencies(this, actorInputController, actorTransformHandler);
                state.OnNewAnimation += SetNewAnimation;
            }
        }
        public void SetCurrentState(IStateComponent newState)
        {
            if (_currentState != null && !_currentState.IsLocked)
                return;
            
            _currentState?.Exit();
            _previousState = _currentState;
            _currentState = newState;
            _currentState.Enter();
        }

        public void SetPreviousState() => SetCurrentState(_previousState);

        public void Tick()
        {
            foreach (var state in states)
                state.Tick();
        }

        private void OnDestroy()
        {
            foreach (var state in states)
                state.OnNewAnimation -= SetNewAnimation;
        }

        private void SetNewAnimation(int animationID) => animator.Play(animationID);
    }
}
