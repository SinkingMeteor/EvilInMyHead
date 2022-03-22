using Sheldier.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryController : SerializedMonoBehaviour, IUIActivatable, ITickListener, IUIInitializable
    {
        [SerializeField] private InventoryView view;
        [OdinSerialize] [ReadOnly] private ICursorRequirer[] cursorRequirers;
        
        private IInventoryInputProvider _inputProvider;
        private UIStatesController _statesController;

        public void Initialize()
        {
            view.Initialize();
            
            _inputProvider.UIOpenInventoryButton.OnPressed += OpenInventoryWindow;
            _inputProvider.UICloseInventoryButton.OnPressed += CloseInventoryWindow;

            for (int i = 0; i < cursorRequirers.Length; i++)
                cursorRequirers[i].SetCursor(_inputProvider);
        }

        [Inject]
        private void InjectDependencies(UIStatesController statesController, IInventoryInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
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
            view.Dispose();
            
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
        
        #if UNITY_EDITOR
        [Button]
        private void FindRequirers()
        {
            cursorRequirers = GetComponentsInChildren<ICursorRequirer>();
        }
        #endif
    }
}

