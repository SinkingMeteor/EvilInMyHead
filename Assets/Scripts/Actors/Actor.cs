using System.Linq;
using Sheldier.Common;
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
        public IActorEffectModule ActorEffectModule => _actorEffectModule;
        public IGrabNotifier GrabNotifier => grabNotifier; 

        [SerializeField] private ActorStateModuleController stateModuleController;
        [OdinSerialize] private ActorEffectModule _actorEffectModule;
        
        [InfoBox("If null, actor can't pick up any objects")]
        [SerializeField] private IGrabNotifier grabNotifier;
        [OdinSerialize] private IExtraActorModule[] modules;

        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private ActorInputController _actorInputController;

        public void Initialize()
        {
            if (grabNotifier == null)
                grabNotifier = new NullGrabModule();
            
            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();
            
            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(transform, _actorInputController);
            
            _actorEffectModule.Initialize();
            
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
        private void InjectDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }


        public void Tick()
        {
            stateModuleController.Tick();
            _actorEffectModule.Tick();
            for (int i = 0; i < modules.Length; i++)
            {
                modules[i].Tick();
            }
        }

        private void OnDestroy()
        {
            if (!GameGlobalSettings.IsStarted) return;
            _tickHandler.RemoveListener(this);
        }
        
    }
}

