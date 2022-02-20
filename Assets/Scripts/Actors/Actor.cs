using System.Linq;
using Sheldier.Common;
using Sheldier.Factories;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;
using IInitializable = Sheldier.Setup.IInitializable;

namespace Sheldier.Actors
{
    public class Actor : SerializedMonoBehaviour, IInitializable, ITickListener
    {
        public ActorInputController InputController => _actorInputController;
        public ActorEffectModule EffectModule => actorEffectModule;
        public ActorNotifyModule Notifier => _notifier;

        [SerializeField] private ActorStateModuleController stateModuleController;
        [OdinSerialize] private ActorEffectModule actorEffectModule;
        [OdinSerialize] private IExtraActorModule[] modules;

        private ActorNotifyModule _notifier;
        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private ActorInputController _actorInputController;
        private ActorsEffectFactory _effectFactory;
        private ItemFactory _itemFactory;
        private ActorInternalData _internalData;
        public void Initialize()
        {
            _notifier = new ActorNotifyModule();
            
            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();
            
            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(transform, _actorInputController);
            
            actorEffectModule.Initialize(_effectFactory);
            
            stateModuleController.SetDependencies(_actorInputController, _transformHandler);

            modules = modules.OrderBy(module => module.Priority).ToArray();

            _internalData = new ActorInternalData(_actorInputController, _transformHandler, actorEffectModule,
                _notifier, _itemFactory);
            
            foreach (var module in modules)
            {
                module.Initialize(_internalData);
            }
            
            _tickHandler.AddListener(this);
        }
        
        [Inject]
        private void InjectDependencies(TickHandler tickHandler, ActorsEffectFactory effectFactory, ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _effectFactory = effectFactory;
            _tickHandler = tickHandler;
        }

        public void Tick()
        {
            stateModuleController.Tick();
            actorEffectModule.Tick();
        }

        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _tickHandler.RemoveListener(this);
        }
        
    }
}

