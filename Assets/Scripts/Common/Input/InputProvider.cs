using System.Collections.Generic;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sheldier.Common
{
    public class InputProvider : MonoBehaviour, IGameplayInputProvider, IInventoryInputProvider, IDialoguesInputProvider
    {
        public Vector2 MovementDirection => playerInput.actions[InputConstants.InputActions[InputActionType.Movement]].ReadValue<Vector2>();
        
        public Vector2 CursorScreenCenterDirection
        {
            get
            {
                var handler = _mouseHandlers[_currentControlScheme];
                var inputAction = playerInput.actions[InputConstants.InputActions[InputActionType.Cursor]];
                var vec = inputAction.ReadValue<Vector2>();
                var direction = handler.GetMouseScreenDirection(vec);
                return direction;
            }
        }
        public InputButton UseButton => _useButton;
        public InputButton AttackButton => _attackButton;
        public InputButton ReloadButton => _reloadButton;
        public InputButton JumpButton => _jumpButton;
        public InputButton DropButton => _dropButton;
        public InputButton UIEquipItemButton => _uiEquipItemButton;
        public InputButton UIOpenInventoryButton => _uiOpenInventoryButton;
        public InputButton UICloseInventoryButton => _uiCloseInventoryButton;
        public InputButton UIUseItemButton => _uiUseItemButton;
        public InputButton UIRemoveItemButton => _uiRemoveItemButton;

        public InputButton LowerChoice => _dialoguesLowerChoice;
        public InputButton LeftChoice => _dialoguesLeftChoice;
        public InputButton UpperChoice => _dialoguesUpperChoice;
        public InputButton RightChoice => _dialoguesRightChoice;
        
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private InputBindHandler bindHandler;
        
        private string _currentControlScheme;
        private CursorDirectionConverter _cursorDirectionConverter;
        private Dictionary<string, IInputMouseHandler> _mouseHandlers;
        private TickHandler _tickHandler;
        
        private InputButton _useButton;
        private InputButton _attackButton;
        private InputButton _reloadButton;
        private InputButton _jumpButton;
        private InputButton _dropButton;

        private InputButton _uiOpenInventoryButton;
        private InputButton _uiCloseInventoryButton;
        private InputButton _uiRemoveItemButton;
        private InputButton _uiEquipItemButton;
        private InputButton _uiUseItemButton;

        private InputButton _dialoguesLowerChoice;
        private InputButton _dialoguesLeftChoice;
        private InputButton _dialoguesUpperChoice;
        private InputButton _dialoguesRightChoice;


        public void Initialize()
        {
            _currentControlScheme = playerInput.currentControlScheme;
            
            _useButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Use]);
            _attackButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Attack]);
            _reloadButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Reload]);
            _jumpButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.Jump]);
            _dropButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.DropItem]);
            
            _uiOpenInventoryButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.OpenInventory]);
            _uiUseItemButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.UseItem]);
            _uiEquipItemButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.EquipItem]);
            _uiRemoveItemButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.RemoveItem]);
            _uiCloseInventoryButton = new InputButton(playerInput, InputConstants.InputActions[InputActionType.CloseInventory]);

            _dialoguesLeftChoice = new InputButton(playerInput, InputConstants.InputActions[InputActionType.DialoguesLeftChoice]);
            _dialoguesUpperChoice = new InputButton(playerInput, InputConstants.InputActions[InputActionType.DialoguesUpperChoice]);
            _dialoguesRightChoice = new InputButton(playerInput, InputConstants.InputActions[InputActionType.DialoguesRightChoice]);
            _dialoguesLowerChoice = new InputButton(playerInput, InputConstants.InputActions[InputActionType.DialoguesLowerChoice]);
            
            _cursorDirectionConverter = new CursorDirectionConverter();
            _mouseHandlers = new Dictionary<string, IInputMouseHandler>
            {
                {InputConstants.PC, new PCMouseDirectionProvider(_cursorDirectionConverter)},
                {InputConstants.GAMEPAD, new GamePadMouseDirectionProvider()},
            };
            ActivateInput();
        }

        public void SwitchActionMap(ActionMapType actionMapType)
        {
            string mapName = InputConstants.ActionMaps[actionMapType];
            var newActionMap = playerInput.actions.FindActionMap(mapName);
            newActionMap.Enable();
            playerInput.currentActionMap.Disable();
            playerInput.currentActionMap = newActionMap;
        }

        public void SetMovementDirection(Vector2 movementDirection)
        { }

        public void SetViewDirection(Vector2 viewDirection)
        { }

        public Vector2 GetNonNormalizedDirectionToCursorFromPosition(Vector3 position)
        {
            var vec = playerInput.actions[InputConstants.InputActions[InputActionType.Cursor]].ReadValue<Vector2>();
            return _cursorDirectionConverter.GetDirectionByTransform(position, _mouseHandlers[_currentControlScheme].GetScreenPointOfCursor(vec));
        }
        public void OnChangedControls(PlayerInput newPlayerInput)
        {
            if (_currentControlScheme == playerInput.currentControlScheme) return;
            _currentControlScheme = playerInput.currentControlScheme;
            bindHandler.OnChangedControls();
            RemoveAllBindingOverrides();
        }
        public void SetSceneCamera(Camera cam)
        {
            _cursorDirectionConverter.SetCamera(cam);
        }
        private void RemoveAllBindingOverrides()
        {
            playerInput.currentActionMap.RemoveAllBindingOverrides();
        }
        private void ActivateInput()
        {
            playerInput.ActivateInput();
            playerInput.actions.FindActionMap(InputConstants.ActionMaps[ActionMapType.Cursor]).Enable();
        }

        private void DeactivateInput()
        {
            playerInput.DeactivateInput();
            playerInput.actions.FindActionMap(InputConstants.ActionMaps[ActionMapType.Cursor]).Disable();
        }
        private void OnDestroy()
        {
            DeactivateInput();
        }

    }
}
