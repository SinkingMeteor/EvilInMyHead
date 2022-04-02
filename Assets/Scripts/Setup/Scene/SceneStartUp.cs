using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Cutscene;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using Sheldier.GameLocation;
using Sheldier.Item;
using Sheldier.UI;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Setup
{
    public class SceneStartUp : MonoBehaviour, ISceneStartup
    {
        [SerializeField] private SceneData sceneData;

        private SceneLoadingOperation _sceneLoadingOperation;
        private ScenePlayerController _scenePlayerController;
        private SceneLocationController _locationController;
        private SceneSetupOperation _sceneSetupOperation;
        private UILoadingOperation _uiLoadingOperation;
        private UIStatesController _uiStatesController;
        private CutsceneController _cutsceneController;
        private InputProvider _inputProvider;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private ActorSpawner _actorSpawner;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation,
             ScenePlayerController scenePlayerController, ActorSpawner actorSpawner, Pathfinder pathfinder,
            UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler, PauseNotifier pauseNotifier, UIStatesController uiStatesController,
             CutsceneController cutsceneController, SceneLocationController locationController,
            SceneSetupOperation sceneSetupOperation)
        {
            _scenePlayerController = scenePlayerController;
            _sceneLoadingOperation = sceneLoadingOperation;
            _sceneSetupOperation = sceneSetupOperation;
            _locationController = locationController;
            _uiStatesController = uiStatesController;
            _uiLoadingOperation = uiLoadingOperation;
            _cutsceneController = cutsceneController;
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _inputProvider = inputProvider;
            _actorSpawner = actorSpawner;
        }

        public async Task StartScene()
        {
            _cameraHandler.InitializeOnScene();
            _inputProvider.SetSceneCamera(_cameraHandler.CurrentSceneCamera);
            _uiStatesController.InitializeOnScene();

            if (_locationController.IsLocationExists)
                await _locationController.DisposeLocation();
            await _locationController.LoadNewLocation(sceneData.SceneStartLocation);
            
            //Test
            Actor firstActor = _actorSpawner.ActorsOnScene.First().Value[0];
            _scenePlayerController.SetControl(firstActor);
            _scenePlayerController.SetFollowTarget(firstActor);



            //  StartCoroutine(DialogueTestCoroutine());
        }

        private void Start()
        {
            GameIsInitialized();
        }

        private IEnumerator DialogueTestCoroutine()
        {
            yield return new WaitForSeconds(7.0f);
            _cutsceneController.StartCutscene(ResourcePaths.TEST_CUTSCENE);

        }
        private void GameIsInitialized()
        {
            if (GameGlobalSettings.IsStarted)
                return;
            _sceneLoadingOperation.SetTargetScene(sceneData);
            _sceneSetupOperation.SetTargetScene(sceneData);
            _uiLoadingOperation.SetTargetScene(sceneData);
            SceneManager.LoadScene("GameEntry");
        }

        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _cameraHandler.OnSceneDispose();
            _uiStatesController.OnSceneDispose();
#pragma warning disable CS4014
            _locationController.DisposeLocation();
#pragma warning restore CS4014
            _pauseNotifier.Clear();
        }
    }
}
