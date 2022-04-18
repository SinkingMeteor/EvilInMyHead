using System;
using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Common.Pause;
using Sheldier.Installers;
using UnityEngine;


namespace Sheldier.UI
{
    public class UIStatesController : IUIStatesController
    {
        private Stack<UIState> _shownStates;
        private Dictionary<UIType, UIState> _states;

        private int _topSortingOrder;
        
        private UIInstaller _uiInstaller;
        private PauseNotifier _pauseNotifier;
        private IGameplayInputProvider _inputProvider;

        public UIStatesController(UIInstaller uiInstaller, PauseNotifier pauseNotifier, IGameplayInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _pauseNotifier = pauseNotifier;
            _uiInstaller = uiInstaller;
        } 
        
        public void InitializeOnScene()
        {
            _shownStates = new Stack<UIState>();
            GameObject uiMain = new GameObject("[UI]");
            uiMain.transform.position = Vector3.zero;
            foreach (var uiState in _states)
            {
                uiState.Value.transform.SetParent(uiMain.transform);
                _uiInstaller.InjectUIState(uiState.Value.gameObject);
            }
            
            _topSortingOrder = 0;
            foreach (var state in _states)
            {
                state.Value.Initialize();
            }
        }
        
        public void SetStates(Dictionary<UIType, UIState> loadedStates)
        {
            _states = loadedStates;
        }

        public bool TryGet<T>(UIType uiType, out T window) where T : MonoBehaviour
        {
            window = null;
            if (!_states.ContainsKey(uiType))
                return false;
            if (_states[uiType].TryGetComponent<T>(out window))
                return true;
            return false;
        }
        
        public void Add(UIType uiType)
        {
            if (!_states.ContainsKey(uiType))
                return;
            
            if(_shownStates.Count > 0)
                _shownStates.Peek().Deactivate();
            
            var state = _states[uiType];
            state.KillAllDisapearingAnimations();
            state.Show();
            state.SetSortingOrder(++_topSortingOrder);
            _shownStates.Push(state);
            if(state.IsRequirePause && !_pauseNotifier.IsPaused)
                _pauseNotifier.Pause();
            _inputProvider.SwitchActionMap(state.ActionMap);
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
                _inputProvider.SwitchActionMap(ActionMapType.Gameplay);
                if(_pauseNotifier.IsPaused)
                    _pauseNotifier.Unpause();
                return;
            };

            var previousState = _shownStates.Peek();
            _inputProvider.SwitchActionMap(previousState.ActionMap);
            if (!previousState.IsRequirePause && _pauseNotifier.IsPaused)
                _pauseNotifier.Unpause();
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