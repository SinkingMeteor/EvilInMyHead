using System;
using UnityEngine.InputSystem;

namespace Sheldier.Common
{
    public class InputButton : IDisposable
    {
        private readonly PlayerInput _playerInput;
        private readonly string _actionKey;
        public event Action OnPressed;
        public event Action OnReleased;

        public InputButton(PlayerInput playerInput, string actionKey)
        {
            _playerInput = playerInput;
            _actionKey = actionKey;
            _playerInput.actions[actionKey].started += OnButtonPressed;
            _playerInput.actions[actionKey].canceled += OnButtonReleased;
        }

        public InputButton()
        {
        }

        private void OnButtonPressed(InputAction.CallbackContext callbackContext)
        {
            OnPressed?.Invoke();
        }

        private void OnButtonReleased(InputAction.CallbackContext callbackContext)
        {
            OnReleased?.Invoke();
        }

        public void InvokeButtonPressedEvent() => OnPressed?.Invoke();
        public void InvokeButtonReleasedEvent() => OnReleased?.Invoke();

        public void Dispose()
        {
            _playerInput.actions[_actionKey].started -= OnButtonPressed;
            _playerInput.actions[_actionKey].canceled -= OnButtonReleased;
        }
    }
}