using System.Collections.Generic;
using Sheldier.Common;
using Zenject;


namespace Sheldier.UI
{
    public class UIStatesController : ITickListener, IUIStatesController
    {
        public UIState CurrentTopState => _currentTopState;
        public IEnumerable<UIState> States => _states;
        
        private UIState[] _states;
        
        private UIState _currentTopState;
        private TickHandler _tickHandler;

        public void InitializeOnScene()
        {
            foreach (var state in _states)
            {
                state.Initialize();
            }

            _currentTopState = _states[0];
            _tickHandler.AddListener(this);
        }

        [Inject]
        private void InjectDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        public void Tick()
        {
            _currentTopState.Tick();
        }
        public void SetStates(UIState[] states) => _states = states;
        
        public void Add(UIState state)
        {
            if (state != null)
            {
                
            }
        }

        public void Remove(UIState state)
        {
        }
        public void OnSceneDispose()
        {
            _tickHandler.RemoveListener(this);
            foreach (var state in _states)
            {
                state.Dispose();
            }
            
        }

 
    }

    public interface IUIStatesController
    {
        void Add(UIState state);
        void Remove(UIState state);
    }
}