using System;
using System.Collections.Generic;
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
        private List<IDeviceListener> _deviceListeners;
        public void Initialize()
        {
            _deviceListeners = new List<IDeviceListener>();
        }
        public Sprite GetActionInputSprite(InputActionType actionType)
        {
            InputAction action = GetInputAction(actionType);
            int controlBindingIndex = 0;
            try
            {
               controlBindingIndex = action.GetBindingIndexForControl(action.controls[0]);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }

            string currentBinding = InputControlPath.ToHumanReadableString(
                action.bindings[controlBindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
            string devicePath = playerInput.devices[0].ToString();
            Sprite actionSprite = deviceKeyMap.GetInputIcon(devicePath, currentBinding);
            return actionSprite;
        }

        public void AddListener(IDeviceListener deviceListener) => _deviceListeners.Add(deviceListener);
        public void RemoveListener(IDeviceListener deviceListener) => _deviceListeners.Remove(deviceListener);

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
        public void OnChangedControls()
        {
            if(_deviceListeners == null) return;
            
            for (int i = 0; i < _deviceListeners.Count; i++)
                _deviceListeners[i].OnDeviceChanged();
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