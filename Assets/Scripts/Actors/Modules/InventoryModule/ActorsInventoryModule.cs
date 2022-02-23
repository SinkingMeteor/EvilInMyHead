using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class ActorsInventoryModule
    {
        public IActorsInventory CurrentInventory => _currentInventory;
        
        private IActorsInventory _currentInventory;
        private ActorNotifyModule _actorNotifyModule;

        public void Initialize(ActorNotifyModule actorNotifyModule)
        {
            _actorNotifyModule = actorNotifyModule;
            _currentInventory = new NullActorsInventory(_actorNotifyModule);
        }
        public bool IsItemExists(ItemConfig itemConfig)
        {
            return _currentInventory.IsItemExists(itemConfig);
        }

        public void AddItem(SimpleItem item)
        {
            _currentInventory.AddItem(item);
        }

        public int RemoveItem(SimpleItem item, int amount = 1)
        {
            return _currentInventory.RemoveItem(item, amount);
        }
        
        public int RemoveItem(ItemConfig item, int amount = 1)
        {
            return _currentInventory.RemoveItem(item, amount);
        }
        public void SetInventory(IActorsInventory actorsInventory)
        {
            _currentInventory = actorsInventory;
        }

        public void RemoveInventory()
        {
            _currentInventory = new NullActorsInventory(_actorNotifyModule);
        }
    }
}