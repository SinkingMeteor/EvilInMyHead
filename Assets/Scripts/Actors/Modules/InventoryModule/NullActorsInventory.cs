using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class NullActorsInventory : IActorsInventory
    {
        private readonly ActorNotifyModule _actorNotifyModule;

        public NullActorsInventory(ActorNotifyModule actorNotifyModule)
        {
            _actorNotifyModule = actorNotifyModule;
        }

        public bool IsItemExists(SimpleItem item) => true;

        public void AddItem(SimpleItem item)
        {
            _actorNotifyModule.NotifyAddedItemToInventory(item);
        }

        public void RemoveItem(SimpleItem item)
        {
        }

        public void SetOwner(Actor actor)
        {
            
        }
    }
}