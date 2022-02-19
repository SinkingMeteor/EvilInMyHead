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
        [SerializeField] private Actor actor;
        [SerializeField] private SceneCameraHandler sceneCameraHandler;
        [SerializeField] private ScenePlaceholdersKeeper scenePlaceholdersKeeper;
        
        private InputProvider _inputProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private ItemSpawner _itemSpawner;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation, ItemSpawner itemSpawner)
        {
            _itemSpawner = itemSpawner;
            _sceneLoadingOperation = sceneLoadingOperation;
            _inputProvider = inputProvider;
        }

        private void Start()
        {
            if (!GameIsInitialized())
                return;
            
            sceneCameraHandler.Initialize(_inputProvider);
            _inputProvider.SetSceneCamera(sceneCameraHandler.CurrentSceneCamera);
            sceneCameraHandler.SetFollowTarget(actor.transform);
            _itemSpawner.Initialize(scenePlaceholdersKeeper);
            actor.Initialize();
            actor.ActorInputController.SetInputProvider(_inputProvider);
            actor.ActorEffectModule.AddEffect(ActorEffectType.Freeze);
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
