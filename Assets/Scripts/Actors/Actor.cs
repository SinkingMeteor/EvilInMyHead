using Sheldier.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using IInitializable = Sheldier.Setup.IInitializable;

namespace Sheldier.Actors
{
    public class Actor : SerializedMonoBehaviour, IInitializable, ITickListener
    {
        [SerializeField] private ActorStateController stateController;

        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private ActorInputController _actorInputController;
        private IInputProvider _inputProvider;

        public void Initialize()
        {

            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();
            _actorInputController.SetInputProvider(_inputProvider);
            
            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(this.transform, _actorInputController);
            
            stateController.SetDependencies(_actorInputController, _transformHandler);
            
            _tickHandler.AddListener(this);
        }

        [Inject]
        private void SetDependencies(TickHandler tickHandler, IInputProvider inputProvider)
        {
            _tickHandler = tickHandler;
            _inputProvider = inputProvider;
        }

        public bool Tick()
        {
            stateController.Tick();
            _transformHandler.Tick();
            return true;
        }
    }

    public class ActorInputController
    {
        public IInputProvider CurrentInputProvider => _currentInputProvider;

        private IInputProvider _currentInputProvider;
        private IInputProvider _nullProvider;
        private IInputProvider _realInputProvider;
        

        public void Initialize()
        {
            _nullProvider = new NullInputProvider();
            _currentInputProvider = _nullProvider;
        }

        public void SetInputProvider(IInputProvider inputProvider)
        {
            _realInputProvider = inputProvider;
            _currentInputProvider = _realInputProvider;
        }
        public void LockInput()
        {
            _currentInputProvider = _nullProvider;
        }

        public void UnlockInput()
        {
            _currentInputProvider = _realInputProvider;
        }
    }
    
}