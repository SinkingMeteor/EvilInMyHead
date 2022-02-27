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
        private SpriteRenderer _spriteRenderer;
        private ScenePlayerController _playerSceneController;
        private Material _defaultMaterial;
        public void Initialize(ActorInternalData data)
        {
            _currentActor = data.Actor;
            _spriteRenderer = data.Sprite;
            _defaultMaterial = data.Sprite.sharedMaterial;
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
            _spriteRenderer.sharedMaterial = material;
        }

        public void OnInteracted(Actor actor)
        {            
            Debug.Log("Replaced");
            _playerSceneController.SetControl(_currentActor);
            _playerSceneController.SetFollowTarget(_currentActor);
        }

        public void OnExit()
        {
            _spriteRenderer.sharedMaterial = _defaultMaterial;

        }
        public void Dispose()
        {
            
        }
    }
}