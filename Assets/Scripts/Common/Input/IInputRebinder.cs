using System;
using UnityEngine.InputSystem;

namespace Sheldier.Common
{
    public interface IInputRebinder
    {
        void StartRebinding(InputActionType actionType,
            Action<InputActionRebindingExtensions.RebindingOperation> onComplete);

        void ResetBindings(InputActionType actionType);
    }
}