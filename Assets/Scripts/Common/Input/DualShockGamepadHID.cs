using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

namespace Sheldier.Common
{
   /* [InputControlLayout(stateType = typeof(DualShock4HIDInputReport))]
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class DualShockGamepadHID : Gamepad
    {
        static DualShockGamepadHID()
        {
            InputSystem.RegisterLayout<DualShockGamepadHID>("PS4GamepadLayout",
                new InputDeviceMatcher()
                    .WithInterface("HID")
                    .WithManufacturer("Sony Interactive Entertainment")
                    .WithProduct("Wireless Controller"));
        
        }
    
        [RuntimeInitializeOnLoadMethod]
        static void Init() {}
    }*/
}