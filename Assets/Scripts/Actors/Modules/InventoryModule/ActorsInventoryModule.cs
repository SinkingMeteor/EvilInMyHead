using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class ActorsInventoryModule
    {
        public IActorsInventory CurrentInventory => _currentInventory;
        
        private IActorsInventory _currentInventory;
        private ActorNotifyModule _actorNotifyModule;

        public void Initialize()
        {
            _currentInventory = new NullActorsInventory();
        }
        public bool IsItemExists(ItemConfig itemConfig)
        {
            return _currentInventory.IsItemExists(itemConfig);
        }

        public bool AddItem(SimpleItem item)
        {
            return _currentInventory.AddItem(item);
        }

        public void RemoveItem(SimpleItem item)
        {
            _currentInventory.RemoveItem(item);
        }
        
        public int RemoveItem(ItemConfig item, int amount = 1, int index = 0)
        {
            return _currentInventory.RemoveItem(item, amount, index);
        }
        public void SetInventory(IActorsInventory actorsInventory)
        {
            _currentInventory = actorsInventory;
        }

        public void RemoveInventory()
        {
            _currentInventory = new NullActorsInventory();
        }
    }
}