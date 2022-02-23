using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class NullActorsInventory : IActorsInventory
    {
        private readonly ActorNotifyModule _actorNotifyModule;

        private const int LARGE_NUMBER = 9999;
        public NullActorsInventory(ActorNotifyModule actorNotifyModule)
        {
            _actorNotifyModule = actorNotifyModule;
        }

        public bool IsItemExists(ItemConfig itemConfig) => true;

        public void AddItem(SimpleItem item)
        {
            _actorNotifyModule.NotifyAddedItemToInventory(item);
        }

        public int RemoveItem(SimpleItem item, int amount = 1)
        {
            return LARGE_NUMBER;
        }

        public int RemoveItem(ItemConfig item, int amount = 1)
        {
            return LARGE_NUMBER;
        }

        public void SetOwner(Actor actor)
        {
            
        }
    }
}