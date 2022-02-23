namespace Sheldier.Item
{
    public abstract class SimpleItem
    {
        public ItemConfig ItemConfig => _itemConfig;
        
        protected readonly ItemConfig _itemConfig;

        public SimpleItem(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
        }

        public abstract void PutToInventory();

        public abstract void Drop();

        public abstract void Equip();

        public abstract void Unequip();
    }
}