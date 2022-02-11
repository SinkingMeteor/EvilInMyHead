using System;
using UnityEngine.InputSystem;

namespace Sheldier.Common
{
    public class InputButton
    {
        private readonly PlayerInput _playerInput;
        public event Action OnPressed;
        public event Action OnReleased;

        public InputButton(PlayerInput playerInput, string actionKey)
        {
            _playerInput = playerInput;
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
    }
}