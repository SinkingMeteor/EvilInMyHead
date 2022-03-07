using System;
using System.Linq;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Common.Pause;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;
using IInitializable = Sheldier.Setup.IInitializable;

namespace Sheldier.Actors
{
    public class Actor : SerializedMonoBehaviour, IInitializable, ITickListener, IFixedTickListener, IPausable
    {
        public event Action OnWillRemoveControl;
        public event Action OnAddedControl;
        
        public ActorInputController InputController => _actorInputController;
        public ActorEffectModule EffectModule => actorEffectModule;
        public ActorNotifyModule Notifier => _notifier;
        public ActorsInventoryModule InventoryModule => _inventoryModule;
        public ActorsView ActorsView => actorsView;

        [SerializeField] private ActorStateModuleController stateModuleController;
        [OdinSerialize] private ActorEffectModule actorEffectModule;
        [OdinSerialize] private IExtraActorModule[] modules;
        [SerializeField] private Rigidbody2D actorsRigidbody;
        [SerializeField] private ActorsView actorsView;

        private ActorNotifyModule _notifier;
        private ActorTransformHandler _transformHandler;
        private ActorInputController _actorInputController;
        private ActorInternalData _internalData;
        private ActorsInventoryModule _inventoryModule;
        private TickHandler _tickHandler;
        private FixedTickHandler _fixedTickHandler;
        private PauseNotifier _pauseNotifier;

        public void Initialize()
        {
            _pauseNotifier.Add(this);
            
            _notifier = new ActorNotifyModule();
            
            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();
            
            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(transform, _actorInputController);
            
            actorEffectModule.Initialize();


            _inventoryModule = new ActorsInventoryModule();
            _inventoryModule.Initialize();

            modules = modules.OrderBy(module => module.Priority).ToArray();

            _internalData = new ActorInternalData(_transformHandler,_tickHandler, this, actorsRigidbody);

            stateModuleController.SetDependencies(_internalData);
            
            foreach (var module in modules)
            {
                module.Initialize(_internalData);
            }
            
            _tickHandler.AddListener(this);
            _fixedTickHandler.AddListener(this);
        }
        
        [Inject]
        private void InjectDependencies(TickHandler tickHandler, FixedTickHandler fixedTickHandler, PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
            _fixedTickHandler = fixedTickHandler;
            _tickHandler = tickHandler;
        }

        public void Tick()
        {
            stateModuleController.Tick();
            actorEffectModule.Tick();
        }
        public void FixedTick()
        {
            stateModuleController.FixedTick();
        }
        public void SetControl(IInputProvider inputProvider)
        {
            _actorInputController.SetInputProvider(inputProvider);
            actorsRigidbody.bodyType = RigidbodyType2D.Dynamic;
            OnAddedControl?.Invoke();
        }

        public void RemoveControl()
        {
            OnWillRemoveControl?.Invoke();
            _actorInputController.RemoveInputProvider();
            actorsRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void SetInventory(IActorsInventory inventory)
        {
            _inventoryModule.SetInventory(inventory);
        }

        public void RemoveInventory()
        {
            _inventoryModule.RemoveInventory();
        }
        public void Pause()
        {
            _tickHandler.RemoveListener(this);
            _fixedTickHandler.RemoveListener(this);
            _actorInputController.LockInput();
        }

        public void Unpause()
        {
            _tickHandler.AddListener(this);
            _fixedTickHandler.AddListener(this);
            _actorInputController.UnlockInput();
        }
        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _pauseNotifier.Remove(this);
            _tickHandler.RemoveListener(this);
            _fixedTickHandler.RemoveListener(this);
            actorEffectModule.Dispose();
            foreach (var module in modules)
            {
                module.Dispose();
            }
        }


    }
}

