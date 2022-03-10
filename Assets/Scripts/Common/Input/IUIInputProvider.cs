namespace Sheldier.Common
{
    public interface IUIInputProvider
    {
        InputButton UIUseItemButton { get; }
        InputButton UIRemoveItemButton { get; }
    }
}