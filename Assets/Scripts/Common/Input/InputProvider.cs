using Sheldier.Constants;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Sheldier.Common
{
    public class InputProvider : MonoBehaviour, IInitializable, IInputProvider
    {
        public Vector2 MovementDirection => playerInput.actions[InputActionNames.MOVEMENT].ReadValue<Vector2>();
        public Vector2 CursorPosition => playerInput.actions[InputActionNames.CURSOR].ReadValue<Vector2>();

        public InputButton UseButton => _useButton;

        [SerializeField] private PlayerInput playerInput;
        private string _currentControlScheme;
        private InputButton _useButton;

        public void Initialize()
        {
            _currentControlScheme = playerInput.currentControlScheme;
            _useButton = new InputButton(playerInput, InputActionNames.USE);
            playerInput.onControlsChanged += OnChangedControls;
            ActivateInput();
        }

        private void OnChangedControls(PlayerInput input)
        {
            if (_currentControlScheme == input.currentControlScheme) return;
            
            playerInput = input;
            _currentControlScheme = playerInput.currentControlScheme;
            RemoveAllBindingOverrides();
        }
        private void RemoveAllBindingOverrides()
        {
            playerInput.currentActionMap.RemoveAllBindingOverrides();
        }
        private void ActivateInput()
        {
            playerInput.ActivateInput();
        }

        private void DeactivateInput()
        {
            playerInput.DeactivateInput();

        }
        private void OnDestroy()
        {
            playerInput.onControlsChanged -= OnChangedControls;
            DeactivateInput();
        }
    }
}
