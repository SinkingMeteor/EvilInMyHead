using UnityEngine;

namespace Sheldier.Common
{
    public interface IUIInputProvider
    {
        Vector2 CursorScreenCenterDirection { get; }
        InputButton UIUseItemButton { get; }
        InputButton UIRemoveItemButton { get; }
        
        InputButton UIOpenInventoryButton { get; }
    }
}