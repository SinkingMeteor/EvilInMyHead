using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.Common;
using Sheldier.Common.Pause;
using Sheldier.Installers;
using UnityEngine;
using Zenject;


namespace Sheldier.UI
{
    public class UIStatesController : IUIStatesController
    {
        public IReadOnlyDictionary<UIType, UIState> States => _states;
        private Stack<UIState> _shownStates;
        private Dictionary<UIType, UIState> _states;
        private Dictionary<UIType, UIState> _loadedStates;

        private UIInstaller _uiInstaller;
        private int _topSortingOrder;
        private PauseNotifier _pauseNotifier;
        private IInputProvider _inputProvider;

        public void InitializeOnScene()
        {
            _shownStates = new Stack<UIState>();
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
        private void InjectDependencies(UIInstaller uiInstaller, PauseNotifier pauseNotifier, IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
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
            
            if(_shownStates.Count > 0)
                _shownStates.Peek().Deactivate();
            
            var state = _states[uiType];
            state.KillAllDisapearingAnimations();
            state.Show();
            state.SetSortingOrder(++_topSortingOrder);
            _shownStates.Push(state);
            if(state.IsRequirePause && !_pauseNotifier.IsPaused)
                _pauseNotifier.Pause();
          //  _inputProvider.SwitchActionMap(state.ActionMap);
        }

        public void Remove(UIType uiType)
        {
            if (!_states.ContainsKey(uiType))
                throw new ArgumentNullException($"UI State {uiType.ToString()} doesn't exist in current scene");
            if (_shownStates.Peek() != _states[uiType])
                throw new ArgumentException($"UI State {uiType.ToString()} is not topmost");
            _topSortingOrder--;
            var state = _shownStates.Pop();
            state.KillAllAppearingAnimations();
            state.Hide();
            
            if(_shownStates.Count == 0)
            {
                if(_pauseNotifier.IsPaused)
                    _pauseNotifier.Unpause();
                return;
            };

            var previousState = _shownStates.Peek();
            if (!previousState.IsRequirePause && _pauseNotifier.IsPaused)
                _pauseNotifier.Unpause();
           // _inputProvider.SwitchActionMap(previousState.ActionMap);
            previousState.Activate();

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