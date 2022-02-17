using System.Linq;
using Sheldier.Common;
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
        
        [SerializeField] private ActorStateModuleController stateModuleController;
        [SerializeField] private IActorEffectModule _actorEffectModule;
        [OdinSerialize] private IActorModule[] modules;
        
        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private ActorInputController _actorInputController;
        public bool WantsToRemoveFromTick => _wantsToRemoveFromTick;
        private bool _wantsToRemoveFromTick;


        public void Initialize()
        {

            _actorInputController = new ActorInputController();
            _actorInputController.Initialize();
            
            _transformHandler = new ActorTransformHandler();
            _transformHandler.SetDependencies(transform, _actorInputController);

            if (_actorEffectModule == null)
                _actorEffectModule = new NullEffectModule();
            
            stateModuleController.SetDependencies(_actorInputController, _transformHandler);

            modules = modules.OrderBy(module => module.Priority).ToArray();
            
            foreach (var module in modules)
            {
                module.Initialize(this);
            }
            
            _tickHandler.AddListener(this);
        }

        [Inject]
        private void InjectDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }


        public void Tick()
        {
            stateModuleController.Tick();

            for (int i = 0; i < modules.Length; i++)
            {
                modules[i].Tick();
            }
        }

        private void OnDestroy()
        {
            OnTickDispose();
        }

        public void OnTickDispose()
        {
            _wantsToRemoveFromTick = true;
        }
    }
}

