using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.Common.Pause;
using Sheldier.Installers;
using UnityEngine;
using Zenject;


namespace Sheldier.UI
{
    public class UIStatesController : IUIStatesController
    {
        public IReadOnlyDictionary<UIType, UIState> States => _states;
        
        private Dictionary<UIType, UIState> _states;
        private Dictionary<UIType, UIState> _loadedStates;

        private UIInstaller _uiInstaller;
        private int _topSortingOrder;
        private PauseNotifier _pauseNotifier;

        public void InitializeOnScene()
        {
            
            _states = new Dictionary<UIType, UIState>();
            GameObject uiMain = new GameObject("[UI]");
            uiMain.transform.position = Vector3.zero;
            foreach (var uiState in _loadedStates)
            {
                var state = GameObject.Instantiate(uiState.Value, uiMain.transform, true);
                _uiInstaller.InjectUIState(state.gameObject);
                _states.Add(uiState.Key, state);
            }
            
            _topSortingOrder = 0;
            foreach (var state in _states)
            {
                state.Value.Initialize();
            }

            _loadedStates = null;
        }

        [Inject]
        private void InjectDependencies(UIInstaller uiInstaller, PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
            _uiInstaller = uiInstaller;
        } 
        
        public void SetStates(Dictionary<UIType, UIState> loadedStates)
        {
            _loadedStates = loadedStates;
        }

        public void Add(UIType uiType)
        {
            if (!_states.ContainsKey(uiType))
                throw new ArgumentNullException($"UI State {uiType.ToString()} doesn't exist in current scene");
            var state = _states[uiType];
            state.Activate();
            state.SetSortingOrder(++_topSortingOrder);
            if(state.IsRequirePause && !_pauseNotifier.IsPaused)
                _pauseNotifier.Pause();
        }

        public void Remove(UIType uiType)
        {
            if (!_states.ContainsKey(uiType))
                throw new ArgumentNullException($"UI State {uiType.ToString()} doesn't exist in current scene");
            _topSortingOrder--;
            var state = _states[uiType];
            state.Deactivate();
            if (!_states.Values.Any(x => x.IsActivated && x.IsRequirePause))
                _pauseNotifier.Unpause();
        }
        public void OnSceneDispose()
        {
            foreach (var state in _states)
            {
                state.Value.Dispose();
            }
        }
 
    }
}