using System.Collections.Generic;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Sheldier.Common
{
    public class InputProvider : MonoBehaviour, IInitializable, IInputProvider
    {
        public Vector2 MovementDirection => playerInput.actions[InputActionNames.MOVEMENT].ReadValue<Vector2>();
        
        public Vector2 CursorScreenCenterDirection
        {
            get
            {
                var handler = _mouseHandlers[_currentControlScheme];
                return handler.GetMouseScreenDirection(playerInput.actions[InputActionNames.CURSOR].ReadValue<Vector2>());
            }
        }

        public InputButton UseButton => _useButton;
        public InputButton AttackButton => _attackButton;
        public InputButton ReloadButton => _reloadButton;

        [SerializeField] private PlayerInput playerInput;
        
        private string _currentControlScheme;
        private CursorDirectionConverter _cursorDirectionConverter;
        private Dictionary<string, IInputMouseHandler> _mouseHandlers;
        
        private InputButton _useButton;
        private InputButton _attackButton;
        private InputButton _reloadButton;

        public void Initialize()
        {
            _currentControlScheme = playerInput.currentControlScheme;
            _useButton = new InputButton(playerInput, InputActionNames.USE);
            _attackButton = new InputButton(playerInput, InputActionNames.ATTACK);
            _reloadButton = new InputButton(playerInput, InputActionNames.RELOAD);
            _cursorDirectionConverter = new CursorDirectionConverter();
            _mouseHandlers = new Dictionary<string, IInputMouseHandler>
            {
                {InputControlSchemes.PC, new PCMouseDirectionProvider(_cursorDirectionConverter)},
                {InputControlSchemes.GAMEPAD, new GamePadMouseDirectionProvider()},
            };
            ActivateInput();
            
        }

        public Vector2 GetNonNormalizedDirectionToCursorFromPosition(Vector3 position)
        {
            return _cursorDirectionConverter.GetDirectionByTransform(position,
                playerInput.actions[InputActionNames.CURSOR].ReadValue<Vector2>());
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
        private interface IInputMouseHandler
        {
            Vector2 GetMouseScreenDirection(Vector2 mousePosition);
        }
        
        private class PCMouseDirectionProvider : IInputMouseHandler
        {
            private readonly CursorDirectionConverter _cursorDirectionConverter;

            public PCMouseDirectionProvider(CursorDirectionConverter cursorDirectionConverter)
            {
                this._cursorDirectionConverter = cursorDirectionConverter;
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
