using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public interface IActorsInventory
    {
        bool IsItemExists(SimpleItem item);
        void AddItem(SimpleItem item);
        void RemoveItem(SimpleItem item);

        void SetOwner(Actor actor);
    }
}