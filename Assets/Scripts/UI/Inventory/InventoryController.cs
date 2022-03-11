using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryController : MonoBehaviour, IUIActivatable,ITickListener,IUIInitializable
    {
        [SerializeField] private InventoryView view;
        
        private IUIInputProvider _inputProvider;
        private UIStatesController _statesController;

        public void Initialize(IUIInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _inputProvider.UIOpenInventoryButton.OnPressed += OpenInventoryWindow;
            _inputProvider.UIOpenInventoryButton.OnReleased += CloseInventoryWindow;
        }

        [Inject]
        private void InjectDependencies(UIStatesController statesController)
        {
            _statesController = statesController;
        }

        public void OnActivated()
        {
            view.Activate();
        }

        public void OnDeactivated()
        {
            view.Deactivate();
        }

        public void Tick()
        {
            view.Tick();
        }

        public void Dispose()
        {
            _inputProvider.UIOpenInventoryButton.OnPressed -= OpenInventoryWindow;
            _inputProvider.UIOpenInventoryButton.OnReleased -= CloseInventoryWindow;
        }
        private void CloseInventoryWindow() => _statesController.Remove(UIType.Inventory);
        private void OpenInventoryWindow() => _statesController.Add(UIType.Inventory);
    }
}

