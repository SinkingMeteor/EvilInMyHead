using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryController : MonoBehaviour, IUIElement
    {
        [SerializeField] private InventoryView view;
        
        private Inventory _inventory;
        private IInputProvider _inputProvider;
        private UIStatesController _statesController;

        public void Initialize(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _inputProvider.OpenInventoryButton.OnPressed += OpenInventoryWindow;
            _inputProvider.OpenInventoryButton.OnReleased += CloseInventoryWindow;
        }

        [Inject]
        private void InjectDependencies(Inventory inventory, UIStatesController statesController)
        {
            _statesController = statesController;
            _inventory = inventory;
        }

        public void OnActivated()
        {
            List<SimpleItem> inventoryItems = new List<SimpleItem>();
            foreach (var inventoryGroup in _inventory.ItemsCollection)
                inventoryItems.AddRange(inventoryGroup.Value.Items);
            view.Activate(inventoryItems);
        }

        public void OnDeactivated()
        {
            ApplySelectedItem();
            view.Deactivate();
        }

        private void ApplySelectedItem()
        {
            SimpleItem selectedItem = view.GetCurrentSelectedItem();
            if (selectedItem == null)
                return;
            _inventory.UseItem(selectedItem);
        }

        public void Tick()
        {
            view.Tick();
        }

        public void Dispose()
        {
            _inputProvider.OpenInventoryButton.OnPressed -= OpenInventoryWindow;
            _inputProvider.OpenInventoryButton.OnReleased -= CloseInventoryWindow;
        }
        private void CloseInventoryWindow() => _statesController.Remove(UIType.Inventory);
        private void OpenInventoryWindow() => _statesController.Add(UIType.Inventory);
    }
}

