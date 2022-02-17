using System.Collections.Generic;
using Sheldier.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorStateModuleController : SerializedMonoBehaviour
    {
        [OdinSerialize] private List<IStateComponent> states;
        
        private TickHandler _tickHandler;

        private IStateComponent _previousState;
        private IStateComponent _currentState;
        


        public bool IsCurrentState(IStateComponent component) => component == _currentState;


        public void SetDependencies(ActorInputController actorInputController, ActorTransformHandler actorTransformHandler)
        {
            foreach (var state in states)
            {
                state.SetDependencies(actorInputController, actorTransformHandler);
            }
        }
        private void SetCurrentState(IStateComponent newState)
        {
            if (_currentState != null && _currentState.IsLocked)
                return;
            
            _currentState?.Exit();
            _previousState = _currentState;
            _currentState = newState;
            _currentState.Enter();
        }
        public void Tick()
        {
            _currentState?.Tick();
            
            IStateComponent nextState = null;
            int currentPriority = -1;

            foreach (var state in states)
            {
                if (state.TransitionConditionIsDone && state.Priority > currentPriority)
                {
                    nextState = state;
                    currentPriority = state.Priority;
                }
            }
            
            if(nextState != null && !IsCurrentState(nextState))
                SetCurrentState(nextState);
        }
        

    }
}
