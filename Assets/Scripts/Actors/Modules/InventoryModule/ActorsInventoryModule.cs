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
        
        public bool IsItemExists(SimpleItem item)
        {
            return _currentInventory.IsItemExists(item);
        }

        public void AddItem(SimpleItem item)
        {
            _currentInventory.AddItem(item);
        }

        public void RemoveItem(SimpleItem item)
        {
            _currentInventory.RemoveItem(item);
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