using System.Collections.Generic;
using Sheldier.Common;

namespace Sheldier.Constants
{
    public static class InputConstants
    {
        public const string PC = "PC";
        public const string GAMEPAD = "GamePad";

        public static readonly Dictionary<InputActionType, string> InputActions =
            new Dictionary<InputActionType, string>()
            {
                {InputActionType.Use, "Use"},
                {InputActionType.Attack, "Attack"},
                {InputActionType.Movement, "Movement"},
                {InputActionType.Point, "Point"},
                {InputActionType.Reload, "Reload"},
                {InputActionType.OpenInventory, "OpenInventory"},
                {InputActionType.UseItem, "UseItem"},
                {InputActionType.RemoveItem, "RemoveItem"},

            };
    }
    
}