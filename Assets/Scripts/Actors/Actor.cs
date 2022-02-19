using System.Linq;
using Sheldier.Actors.Inventory;
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
    public class Actor : SerializedMonoBehaviour, IInitializable, ITickListener, IActorModuleCenter
    {
        public ActorInputController ActorInputController => _actorInputController;
        public ActorTransformHandler ActorTransformHandler => _transformHandler;
        public IActorEffectModule ActorEffectModule => actorEffectModule;
        public ActorNotifyModule Notifier => _notifier; 
        public ItemFactory ItemFactory => _itemFactory;

        [SerializeField] private ActorStateModuleController stateModuleController;
        [OdinSerialize] private ActorEffectModule actorEffectModule;

        [OdinSerialize] private IExtraActorModule[] modules;

        private ActorNotifyModule _notifier;
        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private ActorInputController _actorInputController;
        private ActorsEffectFactory _effectFactory;
        private ItemFactory _itemFactory;

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
            
            foreach (var module in modules)
            {
                module.Initialize(this);
            }
            
            _tickHandler.AddListener(this);
        }

        public bool TryGetModule<T>(out T module) where T : class
        {
            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i] is T extraModule)
                {
                    module = extraModule;
                    return true;
                }
            }
            module = null;
            return false;
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

