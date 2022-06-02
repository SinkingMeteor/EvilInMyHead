using System.Collections;
using System.Threading.Tasks;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Cutscene;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using Sheldier.GameLocation;
using Sheldier.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Setup
{
    public class SceneStartUp : MonoBehaviour, ISceneStartup
    {
        [SerializeField] private SceneData sceneData;

        private PostProcessingController _postProcessingController;
        private SceneLoadingOperation _sceneLoadingOperation;
        private SceneLocationController _locationController;
        private SceneSetupOperation _sceneSetupOperation;
        private UILoadingOperation _uiLoadingOperation;
        private UIStatesController _uiStatesController;
        private CutsceneController _cutsceneController;
        private InputProvider _inputProvider;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation, Pathfinder pathfinder,
            UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler, PauseNotifier pauseNotifier, UIStatesController uiStatesController,
             CutsceneController cutsceneController, SceneLocationController locationController,
            SceneSetupOperation sceneSetupOperation, PostProcessingController postProcessingController)
        {
            _postProcessingController = postProcessingController;
            _sceneLoadingOperation = sceneLoadingOperation;
            _sceneSetupOperation = sceneSetupOperation;
            _locationController = locationController;
            _uiStatesController = uiStatesController;
            _uiLoadingOperation = uiLoadingOperation;
            _cutsceneController = cutsceneController;
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _inputProvider = inputProvider;
        }

        public async Task StartScene()
        {
            _cameraHandler.InitializeOnScene();
            _inputProvider.SetSceneCamera(_cameraHandler.CurrentSceneCamera);
            _uiStatesController.InitializeOnScene();
            _postProcessingController.InitializeOnScene();

            if (_locationController.IsLocationExists)
                await _locationController.DisposeLocation();
            await _locationController.LoadNewLocation(sceneData.SceneStartLocation);
        }

        private void Start()
        {
            GameIsInitialized();
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
