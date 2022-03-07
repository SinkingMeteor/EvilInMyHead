using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class ActorsInventoryModule
    {
        public event Action<SimpleItem> OnUseItem;
        public bool IsEquipped => _isEquipped;
        
        private IActorsInventory _currentInventory;
        private bool _isEquipped;

        public void Initialize()
        {
            _currentInventory = new NullActorsInventory();
        }

        private void UseItem(SimpleItem item)
        {
            OnUseItem?.Invoke(item);
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
        
        public int RemoveItem(ItemConfig item, int amount = 1)
        {
            return _currentInventory.RemoveItem(item, amount);
        }
        public void SetInventory(IActorsInventory actorsInventory)
        {
            _currentInventory = actorsInventory;
            _currentInventory.OnItemUse += UseItem;
        }

        public void RemoveInventory()
        {
            _currentInventory.OnItemUse -= UseItem;
            _currentInventory = new NullActorsInventory();
        }

        public void SetEquipped(bool isEquipped) => _isEquipped = isEquipped;
    }
}