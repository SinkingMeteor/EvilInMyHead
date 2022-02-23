using System;
using Sheldier.Actors;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using Sheldier.Item;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Setup
{
    public class SceneEntry : MonoBehaviour
    {
        [SerializeField] private SceneCameraHandler sceneCameraHandler;
        [SerializeField] private ScenePlaceholdersKeeper scenePlaceholdersKeeper;
        
        private InputProvider _inputProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private ItemSpawner _itemSpawner;
        private ScenePlayerController _scenePlayerController;
        private ActorSpawner _actorSpawner;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation,
            ItemSpawner itemSpawner, ScenePlayerController scenePlayerController, ActorSpawner actorSpawner)
        {
            _scenePlayerController = scenePlayerController;
            _itemSpawner = itemSpawner;
            _sceneLoadingOperation = sceneLoadingOperation;
            _inputProvider = inputProvider;
            _actorSpawner = actorSpawner;
        }

        private void Start()
        {
            if (!GameIsInitialized())
                return;
            
            sceneCameraHandler.Initialize();
            _inputProvider.SetSceneCamera(sceneCameraHandler.CurrentSceneCamera);
            _itemSpawner.Initialize(scenePlaceholdersKeeper);
            _actorSpawner.Initialize(scenePlaceholdersKeeper);
            _scenePlayerController.InitializeOnScene(sceneCameraHandler);

            Actor firstActor = _actorSpawner.ActorsOnScene[0];
            _scenePlayerController.SetControl(firstActor);
            _scenePlayerController.SetFollowTarget(firstActor);

        }

        private bool GameIsInitialized()
        {
            if (GameGlobalSettings.IsStarted)
                return true;
            _sceneLoadingOperation.SetTargetScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneNames.GAME_ENTRY);
            return false;
        }
        
    }

}
