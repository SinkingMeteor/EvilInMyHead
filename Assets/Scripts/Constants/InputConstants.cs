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
                {InputActionType.Cursor, "Cursor"},
                {InputActionType.Reload, "Reload"},
                {InputActionType.OpenInventory, "OpenInventory"},
                {InputActionType.CloseInventory, "CloseInventory"},
                {InputActionType.UseItem, "UseItem"},
                {InputActionType.RemoveItem, "RemoveItem"},
                {InputActionType.DialoguesLeftChoice, "LeftChoice"},
                {InputActionType.DialoguesUpperChoice, "UpperChoice"},
                {InputActionType.DialoguesRightChoice, "RightChoice"},
                {InputActionType.DialoguesLowerChoice, "LowerChoice"},

            };

        public static readonly Dictionary<ActionMapType, string> ActionMaps = new Dictionary<ActionMapType, string>()
        {
            {ActionMapType.Gameplay, "Gameplay"},
            {ActionMapType.UI, "UI"},
            {ActionMapType.Inventory, "Inventory"},
            {ActionMapType.Cursor, "Cursor"},
            {ActionMapType.Dialogues, "Dialogues"},
        };
    }
    
}