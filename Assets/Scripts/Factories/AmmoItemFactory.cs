using Sheldier.Item;

namespace Sheldier.Factories
{
    public class AmmoItemFactory
    {
        private readonly ItemMap _itemMap;

        public AmmoItemFactory(ItemMap itemMap)
        {
            _itemMap = itemMap;
        }

        public AmmoItem GetItem(ItemConfig itemConfig)
        {
            var itemType = itemConfig.ItemType;
            return new AmmoItem(_itemMap.AmmoMap[itemType]);
        }
    }
}