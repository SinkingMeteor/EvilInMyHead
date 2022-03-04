using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Inventory;
using Zenject;

namespace Sheldier.Common
{
    public class ScenePlayerController
    {
        public Actor FollowedActor => _followedActor;
        public Actor ControlledActor => _controlledActor;

        private Actor _followedActor;
        private Actor _controlledActor;
        
        private IInputProvider _inputProvider;
        private CameraHandler _cameraHandler;
        private Inventory _inventory;

        [Inject]
        private void InjectDependencies(IInputProvider inputProvider, Inventory inventory, CameraHandler cameraHandler)
        {
            _cameraHandler = cameraHandler;
            _inventory = inventory;
            _inputProvider = inputProvider;
        }

        public void SetFollowTarget(Actor actor)
        {
            _followedActor = actor;
            _cameraHandler.SetFollowTarget(actor.transform);
        }

        public void SetControl(Actor actor)
        {
            if (_controlledActor != null)
            {
                _controlledActor.RemoveControl();
                _controlledActor.RemoveInventory();
            }
            
            _controlledActor = actor;
            
            _controlledActor.SetControl(_inputProvider);
            _controlledActor.SetInventory(_inventory);
        }

        public bool IsCurrentActor(Actor actor) => _controlledActor == actor;
    }

}
