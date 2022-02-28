using Sheldier.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Sheldier.Actors.Interact
{
    public class ReplaceInteractReceiver : SerializedMonoBehaviour, IInteractReceiver, IExtraActorModule
    {
        public Transform Transform => transform;
        public int Priority => 0;

        [SerializeField] private Material material;

        private Actor _currentActor;
        private ScenePlayerController _playerSceneController;
        private Material _defaultMaterial;
        private ActorsView _actorsView;

        public void Initialize(ActorInternalData data)
        {
            _currentActor = data.Actor;
            _actorsView = data.Actor.ActorsView;
            _defaultMaterial = _actorsView.CurrentBodyMaterial;
        }
        [Inject]
        private void InjectDependencies(ScenePlayerController playerSceneController)
        {
            _playerSceneController = playerSceneController;
        }
        public void OnEntered()
        {
            if (_playerSceneController.IsCurrentActor(_currentActor))
                return;
            _actorsView.SetMaterial(material);
        }

        public void OnInteracted(Actor actor)
        {            
            Debug.Log("Replaced");
            _playerSceneController.SetControl(_currentActor);
            _playerSceneController.SetFollowTarget(_currentActor);
        }

        public void OnExit()
        {
            _actorsView.SetMaterial(_defaultMaterial);
        }
        public void Dispose()
        {
            
        }
    }
}