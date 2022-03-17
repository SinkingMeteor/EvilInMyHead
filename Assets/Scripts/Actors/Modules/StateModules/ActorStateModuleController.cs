using System.Collections.Generic;

namespace Sheldier.Actors
{
    public class ActorStateModuleController
    {
        
        private List<IStateComponent> _states;
        
        private IStateComponent _previousState;
        private IStateComponent _currentState;
        private ActorInternalData _data;

        public bool IsCurrentState(IStateComponent component) => component == _currentState;
        
        
        public void Initialize(ActorInternalData data)
        {
            _states = new List<IStateComponent>();
            _data = data;
        }

        public void AddState(IStateComponent stateComponent)
        {
            _states.Add(stateComponent);
            stateComponent.SetDependencies(_data);
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
        public void Pause()
        {
            _currentState?.Exit();
        }
        
        public void Tick()
        {
            _currentState?.Tick();
            
            IStateComponent nextState = null;
            int currentPriority = -1;

            foreach (var state in _states)
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

        public void FixedTick()
        {
            _currentState?.FixedTick();
        }

    }
}
