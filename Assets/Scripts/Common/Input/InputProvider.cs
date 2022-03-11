using System.Collections.Generic;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sheldier.Common
{
    public class InputProvider : MonoBehaviour, IInputProvider, IUIInputProvider
    {
        public Vector2 MovementDirection => playerInput.actions[InputConstants.InputActions[InputActionType.Movement]].ReadValue<Vector2>();
        
        public Vector2 CursorScreenCenterDirection
        {
            get
            {
                var handler = _mouseHandlers[_currentControlScheme];
                return handler.GetMouseScreenDirection(playerInput.actions[InputConstants.InputActions[InputActionType.Point]].ReadValue<Vector2>());
            }
        }

        public InputButton UseButton => _useButton;
        public InputButton AttackButton => _attackButton;
        public InputButton ReloadButton => _reloadButton;
        public InputButton UIOpenInventoryButton => _uiOpenInventoryButton;
        public InputButton UIUseItemButton => _uiUseItemButton;
        public InputButton UIRemoveItemButton => _uiRemoveItemButton;
        
        [SerializeField] private PlayerInput playerInput;
        
        private string _currentControlScheme;
        private CursorDirectionConverter _cursorDirectionConverter;
        private Dictionary<string, IInputMouseHandler> _mouseHandlers;
        
        private InputButton _useButton;
        private InputButton _attackButton;
        private InputButton _reloadButton;
        
        private InputButton _uiOpenInventoryButton;
        private InputButton _uiRemoveItemButton;
        private InputButton _uiUseItemButton;


        public void Initialize()
        {
            _currentControlScheme = playerInput.currentControlScheme;
            _useButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Use]);
            _attackButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Attack]);
            _reloadButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Reload]);
            
            _uiOpenInventoryButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.OpenInventory]);
            _uiUseItemButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.UseItem]);
            _uiRemoveItemButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.RemoveItem]);
            
            _cursorDirectionConverter = new CursorDirectionConverter();
            _mouseHandlers = new Dictionary<string, IInputMouseHandler>
            {
                {InputConstants.PC, new PCMouseDirectionProvider(_cursorDirectionConverter)},
                {InputConstants.GAMEPAD, new GamePadMouseDirectionProvider()},
            };
            ActivateInput();
            
        }

        public Vector2 GetNonNormalizedDirectionToCursorFromPosition(Vector3 position)
        {
            return _cursorDirectionConverter.GetDirectionByTransform(position,
                playerInput.actions[InputConstants.InputActions[InputActionType.Point]].ReadValue<Vector2>());
        }
        public void OnChangedControls(PlayerInput newPlayerInput)
        {
            if (_currentControlScheme == playerInput.currentControlScheme) return;
            
            _currentControlScheme = playerInput.currentControlScheme;
            RemoveAllBindingOverrides();
        }

        public void DeviceRegained()
        {
  
        }

        public void DeviceLost()
        {


        }
        public void SetSceneCamera(Camera camera)
        {
            _cursorDirectionConverter.SetCamera(camera);
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
            DeactivateInput();
        }


        private interface IInputMouseHandler
        {
            Vector2 GetMouseScreenDirection(Vector2 mousePosition);
        }
        private class CursorDirectionConverter
        {
            private Camera _camera;
            public void SetCamera(Camera camera)
            {
                _camera = camera;
            }

            public Vector2 GetMouseScreenDirection(Vector2 mouseScreenPosition)
            {
                var screen = new Vector2(Screen.width, Screen.height);
                var halfScreen = screen * 0.5f;
                var directionMinusOneToOne = (mouseScreenPosition - halfScreen) / screen.y * 2.0f;
                
                return directionMinusOneToOne;
            }

            public Vector2 GetDirectionByTransform(Vector3 position, Vector2 cursorPosition)
            {
                var worldCursorPosition = _camera.ScreenToWorldPoint(cursorPosition);
                return (worldCursorPosition - position);
            }
        }
        private class PCMouseDirectionProvider : IInputMouseHandler
        {
            private readonly CursorDirectionConverter _cursorDirectionConverter;

            public PCMouseDirectionProvider(CursorDirectionConverter cursorDirectionConverter)
            {
                _cursorDirectionConverter = cursorDirectionConverter;
            }
            public Vector2 GetMouseScreenDirection(Vector2 mousePosition)
            {
                return _cursorDirectionConverter.GetMouseScreenDirection(mousePosition);
            }
        }
        private class GamePadMouseDirectionProvider : IInputMouseHandler
        {

            private Vector2 previousDirection;
            public Vector2 GetMouseScreenDirection(Vector2 mouseDirection)
            {
                var newDirection = Vector2.Lerp(previousDirection, mouseDirection, Time.deltaTime);
                previousDirection = newDirection;
                return newDirection;
            }
        }
    }
}
