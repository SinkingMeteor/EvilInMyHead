using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Pause;
using Sheldier.Item;
using Sheldier.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Sheldier.Setup
{
    public class SceneStartUp : MonoBehaviour
    {
        [SerializeField] private ScenePlaceholdersKeeper scenePlaceholdersKeeper;
        [SerializeField] private PathGrid pathfindingGrid;
        [SerializeField] private SceneData sceneData;

        private SceneLoadingOperation _sceneLoadingOperation;
        private ScenePlayerController _scenePlayerController;
        private UILoadingOperation _uiLoadingOperation;
        private UIStatesController _uiStatesController;
        private InputProvider _inputProvider;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private ActorSpawner _actorSpawner;
        private ItemSpawner _itemSpawner;
        private Pathfinder _pathfinder;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation,
            ItemSpawner itemSpawner, ScenePlayerController scenePlayerController, ActorSpawner actorSpawner, Pathfinder pathfinder,
            UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler,
            PauseNotifier pauseNotifier, UIStatesController uiStatesController)
        {
            _scenePlayerController = scenePlayerController;
            _sceneLoadingOperation = sceneLoadingOperation;
            _uiStatesController = uiStatesController;
            _uiLoadingOperation = uiLoadingOperation;
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _inputProvider = inputProvider;
            _actorSpawner = actorSpawner;
            _itemSpawner = itemSpawner;
            _pathfinder = pathfinder;
        }

        private void Start()
        {
            if (!GameIsInitialized())
                return;
            
            _cameraHandler.InitializeOnScene();
            _inputProvider.SetSceneCamera(_cameraHandler.CurrentSceneCamera);
            _itemSpawner.InitializeOnScene(scenePlaceholdersKeeper);
            _actorSpawner.Initialize(scenePlaceholdersKeeper);
            pathfindingGrid.Initialize();
            _pathfinder.InitializeOnScene(pathfindingGrid);
            _uiStatesController.InitializeOnScene();
            
            //Test
            Actor firstActor = _actorSpawner.ActorsOnScene[0];
            _scenePlayerController.SetControl(firstActor);
            _scenePlayerController.SetFollowTarget(firstActor);

        }
        
        private bool GameIsInitialized()
        {
            if (GameGlobalSettings.IsStarted)
                return true;
            _sceneLoadingOperation.SetTargetScene(sceneData);
            _uiLoadingOperation.SetTargetScene(sceneData);
            SceneManager.LoadScene("GameEntry");
            return false;
        }

        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            pathfindingGrid.Dispose();
            _cameraHandler.OnSceneDispose();
            _uiStatesController.OnSceneDispose();
            _pauseNotifier.Clear();
        }
    }

}
