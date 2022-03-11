using System;
using Sheldier.Constants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sheldier.Common
{
    public class InputBindHandler : MonoBehaviour, IInputRebinder, IInputBindIconProvider
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private DeviceKeyMap deviceKeyMap;
        
        private InputActionRebindingExtensions.RebindingOperation _rebindOperation;

        public Sprite GetActionInputSprite(InputActionType actionType)
        {
            InputAction action = GetInputAction(actionType);
            int controlBindingIndex = action.GetBindingIndexForControl(action.controls[0]);
            string currentBinding = InputControlPath.ToHumanReadableString(
                action.bindings[controlBindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
            for (int i = 0; i < action.bindings.Count; i++)
            {
                Debug.Log(InputControlPath.ToHumanReadableString(
                    action.bindings[i].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice));
            }
            string devicePath = playerInput.devices[0].ToString();
            Sprite actionSprite = deviceKeyMap.GetInputIcon(devicePath, currentBinding);
            return actionSprite;
        }
        
        public void ResetBindings(InputActionType actionType) => GetInputAction(actionType).RemoveAllBindingOverrides();

        public void StartRebinding(InputActionType actionType, Action<InputActionRebindingExtensions.RebindingOperation> onComplete)
        {
            _rebindOperation = GetInputAction(actionType).PerformInteractiveRebinding()
                .WithControlsExcluding("<Mouse>/position")
                .WithControlsExcluding("<Mouse>/delta")
                .WithControlsExcluding("<Gamepad>/Start")
                .WithControlsExcluding("<Keyboard>/escape")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation =>
                {
                    RebindCompleted();
                    onComplete(operation);
                });

            _rebindOperation.Start();
        }
        private InputAction GetInputAction(InputActionType actionType)
        {
            return playerInput.actions.FindAction(InputConstants.InputActions[actionType]);
        }
        private void RebindCompleted()
        {
            _rebindOperation.Dispose();
            _rebindOperation = null;
        }
    }
}