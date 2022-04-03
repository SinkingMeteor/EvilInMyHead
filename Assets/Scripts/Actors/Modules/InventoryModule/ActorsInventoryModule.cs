using System;
using Sheldier.Item;

namespace Sheldier.Actors.Inventory
{
    public class ActorsInventoryModule
    {
        public event Action<ItemDynamicConfigData> OnUseItem;
        public bool IsEquipped => _isEquipped;
        
        private IActorsInventory _currentInventory;
        private bool _isEquipped;

        public void Initialize()
        {
            _currentInventory = new NullActorsInventory();
        }

        private void UseItem(ItemDynamicConfigData dynamicConfigData)
        {
            OnUseItem?.Invoke(dynamicConfigData);
        }

        public bool IsItemExists(string guid)
        {
            return _currentInventory.IsItemExists(guid);
        }

        public bool IsItemTypeExists(string typeName)
        {
            return _currentInventory.IsItemTypeExists(typeName);
        }

        public InventoryOperationReport AddItem(ItemDynamicConfigData item)
        {
            return _currentInventory.AddItem(item);
        }

        public InventoryOperationReport RemoveItem(string guid)
        {
            return _currentInventory.RemoveItem(guid);
        }
        
        public InventoryOperationReport RemoveItem(string typeName, int amount = 1)
        {
            return _currentInventory.RemoveItemAmount(typeName, amount);
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