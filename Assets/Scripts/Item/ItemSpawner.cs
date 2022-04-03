using Sheldier.Factories;
using Sheldier.GameLocation;

namespace Sheldier.Item
{
    public class ItemSpawner
    {
        private LocationPlaceholdersKeeper _placeholdersKeeper;
        private ItemFactory _itemFactory;

        public void Initialize(LocationPlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            LoadItems();
        }

        public void SetDependencies(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        
        private void LoadItems()
        {
            foreach (var placeholder in _placeholdersKeeper.ItemPlaceholders)
            {
                ItemDynamicConfigData dynamicConfigData = _itemFactory.CreateItem(placeholder.Reference.Reference);
                placeholder.Initialize(dynamicConfigData);                
            }
        }
    }
}