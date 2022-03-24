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
        public ActorType ActorType => _dataModule.ActorConfig.ActorType;
        public ActorInputController InputController => _actorInputController;
        public ActorNotifyModule Notifier => _notifier;
        public ActorsInventoryModule InventoryModule => _inventoryModule;
        public ActorsView ActorsView => actorsView;
        public ActorStateModuleController StateModuleController => _stateModuleController;
        public ActorDataModule DataModule => _dataModule;
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
        private ActorDataModule _dataModule;
        private RigidbodyType2D _previousBodyType;

        public void Initialize()
        {
            _pauseNotifier.Add(this);
            
            _notifier = new ActorNotifyModule();

            _dataModule = new ActorDataModule();
            
            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();

            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(transform, _actorInputController);

            _inventoryModule = new ActorsInventoryModule();
            _inventoryModule.Initialize();

            actorsView.SetDependencies(_tickHandler);
            actorsView.Initialize();
            
            _internalData = new ActorInternalData(_transformHandler,_tickHandler, this, actorsRigidbody);

            _stateModuleController = new ActorStateModuleController();
            _stateModuleController.Initialize(_internalData);

            _tickHandler.AddListener(this);
            _fixedTickHandler.AddListener(this);

            _extraModules = new List<IExtraActorModule>();
        }
        
        public void SetDependencies(TickHandler tickHandler, FixedTickHandler fixedTickHandler, PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
            _fixedTickHandler = fixedTickHandler;
            _tickHandler = tickHandler;
        }

        public void Say()
        {
            var clip = _dataModule.DialogueDataModule.TypeVoice;
            soundController.PlayAudio(clip);
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
        public void Tick() => _stateModuleController.Tick();
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
            actorsView.Dispose();
            foreach (var module in _extraModules)
            {
                module.Dispose();
            }
        }


    }
}

