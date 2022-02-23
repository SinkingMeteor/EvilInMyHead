using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public interface IActorsInventory
    {
        bool IsItemExists(ItemConfig itemConfig);
        void AddItem(SimpleItem item);
        int RemoveItem(SimpleItem item, int amount);
        int RemoveItem(ItemConfig item, int amount = 1);
        void SetOwner(Actor actor);
    }
}