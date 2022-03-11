using UnityEngine;

namespace Sheldier.Common
{
    public interface IInputBindIconProvider
    {
        Sprite GetActionInputSprite(InputActionType actionType);
    }
}