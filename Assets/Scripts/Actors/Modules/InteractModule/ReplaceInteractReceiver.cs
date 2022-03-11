using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public class ReplaceInteractReceiver : MonoBehaviour, IInteractReceiver, IExtraActorModule
    {
        public Transform Transform => _currentActor.transform;

        private Material _material;
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
        public void SetDependencies(ScenePlayerController playerSceneController, Material material)
        {
            _material = material;
            _playerSceneController = playerSceneController;
        }
        public void OnEntered()
        {
            if (_playerSceneController.IsCurrentActor(_currentActor))
                return;
            _actorsView.SetMaterial(_material);
        }
        public bool OnInteracted(Actor actor)
        {            
            _playerSceneController.SetControl(_currentActor);
            _playerSceneController.SetFollowTarget(_currentActor);
            return false;
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