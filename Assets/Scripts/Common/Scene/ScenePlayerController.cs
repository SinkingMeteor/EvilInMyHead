using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Actors.Inventory;
using Sheldier.Data;
using Zenject;

namespace Sheldier.Common
{
    public class ScenePlayerController
    {
        public string ControlledActorGuid => _controlledActorsGuid;

        private string _controlledActorsGuid;

        private SceneActorsDatabase _sceneActorsDatabase;
        private IGameplayInputProvider _inputProvider;
        private Inventory _inventory;

        [Inject]
        private void InjectDependencies(IGameplayInputProvider inputProvider, Inventory inventory, SceneActorsDatabase sceneActorsDatabase)
        {
            _sceneActorsDatabase = sceneActorsDatabase;
            _inventory = inventory;
            _inputProvider = inputProvider;
        }
        
        public void SetControl(string guid)
        {
            var controlledActor = _sceneActorsDatabase.Get(guid);
            SetControl(controlledActor);
        }

        public void SetControl(Actor controlledActor)
        {
            if (controlledActor != null)
            {
                controlledActor.RemoveControl();
                controlledActor.InventoryModule.RemoveInventory();
            }
            
            controlledActor.SetControl(_inputProvider);
            controlledActor.InventoryModule.SetInventory(_inventory);

            _controlledActorsGuid = controlledActor.Guid;
        }
        
        public bool IsCurrentActor(string guid) => _controlledActorsGuid == guid;
    }

}
