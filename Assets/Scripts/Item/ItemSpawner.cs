using Zenject;

namespace Sheldier.Item
{
    public class ItemSpawner
    {
        private ScenePlaceholdersKeeper _placeholdersKeeper;
        private ItemMap _itemMap;

        public void Initialize(ScenePlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;

            LoadItems();
        }

        [Inject]
        private void InjectDependencies(ItemMap itemMap)
        {
            _itemMap = itemMap;
        }
        
        private void LoadItems()
        {
            foreach (var placeholder in _placeholdersKeeper.ItemPlaceholders)
            {
                if (_itemMap.ItemsMap.TryGetValue(placeholder.Reference, out ItemConfig config))
                    placeholder.Initialize(config);
                else
                    placeholder.Deactivate();                    
            }
        }
    }
}