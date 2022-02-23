using Sheldier.Factories;
using Zenject;

namespace Sheldier.Item
{
    public class ItemSpawner
    {
        private ScenePlaceholdersKeeper _placeholdersKeeper;
        private ItemFactory _itemFactory;

        public void Initialize(ScenePlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;

            LoadItems();
        }

        [Inject]
        private void InjectDependencies(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }
        
        private void LoadItems()
        {
            foreach (var placeholder in _placeholdersKeeper.ItemPlaceholders)
            {
                placeholder.Initialize(_itemFactory.GetItem(placeholder.Reference));                
            }
        }
    }
}