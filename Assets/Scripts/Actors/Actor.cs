using System;
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
        public bool WantsToRemoveFromTick => _wantsToRemoveFromTick;
        private bool _wantsToRemoveFromTick;


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
        private void InjectDependencies(TickHandler tickHandler, IInputProvider inputProvider)
        {
            _tickHandler = tickHandler;
            _inputProvider = inputProvider;
        }


        public void Tick()
        {
            stateController.Tick();
        //    _transformHandler.Tick();
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