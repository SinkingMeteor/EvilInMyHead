using UnityEngine;

namespace Sheldier.Common
{
    public interface IInputBindIconProvider
    {
        Sprite GetActionInputSprite(InputActionType actionType);
        void AddListener(IDeviceListener deviceListener);
        void RemoveListener(IDeviceListener deviceListener);
    }
}