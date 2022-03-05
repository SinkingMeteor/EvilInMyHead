using Sheldier.Actors.Inventory;
using Sheldier.Common.Pool;
using Sheldier.Item;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryView view;
        private Inventory _inventory;

        [Inject]
        private void InjectDependencies(Inventory inventory)
        {
            _inventory = inventory;
        }
    }

    public class InventorySlot : MonoBehaviour, IPoolObject<InventorySlot>
    {
        public Transform Transform => transform;

        [SerializeField] private Image itemIcon; 
        
        private ItemConfig _itemConfig;

        public void Initialize(IPoolSetter<InventorySlot> poolSetter)
        {
        }

        public void SetItem(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
        }
        public void OnInstantiated()
        {
        }

        public void Reset()
        {
            
        }
    }

    public class InventoryView : MonoBehaviour
    {
        
    }
}

