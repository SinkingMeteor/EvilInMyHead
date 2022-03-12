
namespace Sheldier.Common
{
    public interface IInventoryInputProvider : ICursorProvider
    {
        InputButton UIUseItemButton { get; }
        InputButton UIRemoveItemButton { get; }
        InputButton UIOpenInventoryButton { get; }
        InputButton UICloseInventoryButton { get; }
        
    }
}