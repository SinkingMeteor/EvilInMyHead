using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryController : MonoBehaviour, IUIActivatable, ITickListener, IUIInitializable
    {
        [SerializeField] private InventoryView view;
        
        private IInventoryInputProvider _inputProvider;
        private UIStatesController _statesController;

        public void Initialize(IInventoryInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _inputProvider.UIOpenInventoryButton.OnPressed += OpenInventoryWindow;
            _inputProvider.UICloseInventoryButton.OnPressed += CloseInventoryWindow;
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
            _inputProvider.UICloseInventoryButton.OnPressed -= CloseInventoryWindow;

        }
        private void CloseInventoryWindow()
        {
            _statesController.Remove(UIType.Inventory);
        }

        private void OpenInventoryWindow()
        {
            _statesController.Add(UIType.Inventory);
        }
    }
}

