using System;
using System.Collections.Generic;
using Sheldier.Constants;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Common
{
    public class InputProvider : MonoBehaviour, IInitializable, IInputProvider
    {
        public Vector2 MovementDirection => playerInput.actions[InputActionNames.MOVEMENT].ReadValue<Vector2>();

        public Vector2 CursorScreenDirection
        {
            get
            {
                var handler = _mouseHandlers[_currentControlScheme];
                return handler.GetMouseScreenDirection(playerInput.actions[InputActionNames.CURSOR].ReadValue<Vector2>());
            }
        }

        public InputButton UseButton => _useButton;

        [SerializeField] private PlayerInput playerInput;
        
        private string _currentControlScheme;
        private InputButton _useButton;
        private CursorDirectionConverter _cursorDirectionConverter;
        private Dictionary<string, IInputMouseHandler> _mouseHandlers;
        public void Initialize()
        {
            _currentControlScheme = playerInput.currentControlScheme;
            _useButton = new InputButton(playerInput, InputActionNames.USE);
            _cursorDirectionConverter = new CursorDirectionConverter();
            _mouseHandlers = new Dictionary<string, IInputMouseHandler>
            {
                {InputControlSchemes.PC, new PCMouseDirectionProvider(_cursorDirectionConverter)},
                {InputControlSchemes.GAMEPAD, new GamePadMouseDirectionProvider()},
            };
            ActivateInput();
            
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
                var directionMinusOneToOne = (mouseScreenPosition - halfScreen) / screen * 2.0f;
                
                return directionMinusOneToOne;
            }
            public Vector2 GetMouseWorldPosition(Vector2 mouseScreenPosition)
            {
                return _camera.ScreenToWorldPoint(mouseScreenPosition);
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
