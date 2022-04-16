using System;
using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Common.Pause;
using Sirenix.OdinInspector;
using UnityEngine;
using IInitializable = Sheldier.Setup.IInitializable;

namespace Sheldier.Actors
{
    public class Actor : SerializedMonoBehaviour, IInitializable, ITickListener, IFixedTickListener, IPausable
    {
        public event Action OnWillRemoveControl;
        public event Action OnAddedControl;
        public string Guid => _guid;
        public ActorInputController InputController => _actorInputController;
        public ActorNotifyModule Notifier => _notifier;
        public ActorsInventoryModule InventoryModule => _inventoryModule;
        public ActorsView ActorsView => actorsView;
        public ActorStateModuleController StateModuleController => _stateModuleController;
        public ActorStateDataModule StateDataModule => _dataModule;
        public ActorSoundController SoundController => soundController;

        [SerializeField] private Rigidbody2D actorsRigidbody;
        [SerializeField] private ActorsView actorsView;
        [SerializeField] private ActorSoundController soundController;

        private List<IExtraActorModule> _extraModules;
        private ActorStateModuleController _stateModuleController;
        private ActorNotifyModule _notifier;
        private ActorTransformHandler _transformHandler;
        private ActorInputController _actorInputController;
        private ActorInternalData _internalData;
        private ActorsInventoryModule _inventoryModule;
        private TickHandler _tickHandler;
        private FixedTickHandler _fixedTickHandler;
        private PauseNotifier _pauseNotifier;
        private ActorStateDataModule _dataModule;
        private RigidbodyType2D _previousBodyType;
        private ActorTraceProvider _traceProvider;
        private string _guid;

        public void Initialize()
        {
            _pauseNotifier.Add(this);
            
            _notifier = new ActorNotifyModule();

            _dataModule = new ActorStateDataModule();
            
            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();

            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(transform, _actorInputController);

            _inventoryModule = new ActorsInventoryModule();
            _inventoryModule.Initialize();

            actorsView.SetDependencies(_tickHandler, _dataModule);
            actorsView.Initialize();
            
            _traceProvider = new ActorTraceProvider();
            _traceProvider.SetDependencies(transform);
            _traceProvider.Initialize();
            
            _internalData = new ActorInternalData(_transformHandler,_tickHandler, this, actorsRigidbody, _traceProvider);

            _stateModuleController = new ActorStateModuleController();
            _stateModuleController.Initialize(_internalData);

            _tickHandler.AddListener(this);
            _fixedTickHandler.AddListener(this);

            _extraModules = new List<IExtraActorModule>();
        }
        
        public void SetDependencies(string guid, TickHandler tickHandler, FixedTickHandler fixedTickHandler, PauseNotifier pauseNotifier)
        {
            _guid = guid;
            _pauseNotifier = pauseNotifier;
            _fixedTickHandler = fixedTickHandler;
            _tickHandler = tickHandler;
        }
        public void AddExtraModule(IExtraActorModule extraActorModule)
        {
            _extraModules.Add(extraActorModule);
            extraActorModule.Initialize(_internalData);
        }

        public void RemoveExtraModule(IExtraActorModule extraActorModule)
        {
            if(!_extraModules.Contains(extraActorModule))
                return;
            _extraModules.Remove(extraActorModule);
            extraActorModule.Dispose();
        }
        public void Tick()
        {
            _traceProvider.Tick();
            actorsView.Tick();
            _stateModuleController.Tick();
        }

        public void FixedTick() => _stateModuleController.FixedTick();
        public void SetControl(IGameplayInputProvider inputProvider)
        {
            _actorInputController.SetInputProvider(inputProvider);
            actorsRigidbody.bodyType = RigidbodyType2D.Dynamic;
            OnAddedControl?.Invoke();
        }

        public void RemoveControl()
        {
            OnWillRemoveControl?.Invoke();
            actorsRigidbody.bodyType = RigidbodyType2D.Kinematic;
            _actorInputController.RemoveInputProvider();
        }

        public void LockInput()
        {
            _actorInputController.LockInput();
            _previousBodyType = actorsRigidbody.bodyType;
            actorsRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void UnlockInput()
        {
            _actorInputController.UnlockInput();
            actorsRigidbody.bodyType = _previousBodyType;
        }
        public void Pause()
        {
            _tickHandler.RemoveListener(this);
            _fixedTickHandler.RemoveListener(this);
            _actorInputController.LockInput();
            _stateModuleController.Pause();
        }

        public void Unpause()
        {
            _tickHandler.AddListener(this);
            _fixedTickHandler.AddListener(this);
            _actorInputController.UnlockInput();
        }
        public void Dispose()
        {
            _pauseNotifier.Remove(this);
            _tickHandler.RemoveListener(this);
            _fixedTickHandler.RemoveListener(this);
            _stateModuleController.Dispose();
            actorsView.Dispose();
            foreach (var module in _extraModules)
            {
                module.Dispose();
            }
        }


    }
}

