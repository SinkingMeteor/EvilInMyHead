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
    public class SceneEntry : MonoBehaviour
    {
        [SerializeField] private ScenePlaceholdersKeeper scenePlaceholdersKeeper;
        [SerializeField] private PathGrid pathfindingGrid;
        [SerializeField] private SceneData sceneData;
        
        private InputProvider _inputProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private ItemSpawner _itemSpawner;
        private ScenePlayerController _scenePlayerController;
        private ActorSpawner _actorSpawner;
        private Pathfinder _pathfinder;
        private UILoadingOperation _uiLoadingOperation;
        private UIStatesController _uiStatesController;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;

        [Inject]
        public void InjectDependencies(InputProvider inputProvider, SceneLoadingOperation sceneLoadingOperation,
            ItemSpawner itemSpawner, ScenePlayerController scenePlayerController, ActorSpawner actorSpawner, Pathfinder pathfinder,
            UILoadingOperation uiLoadingOperation, UIStatesController uiStatesController, CameraHandler cameraHandler,
            PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _uiStatesController = uiStatesController;
            _uiLoadingOperation = uiLoadingOperation;
            _pathfinder = pathfinder;
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
            
            _cameraHandler.InitializeOnScene();
            _inputProvider.SetSceneCamera(_cameraHandler.CurrentSceneCamera);
            _itemSpawner.Initialize(scenePlaceholdersKeeper);
            _actorSpawner.Initialize(scenePlaceholdersKeeper);
            pathfindingGrid.Initialize();
            _pathfinder.InitializeOnScene(pathfindingGrid);

            InstantiateUI();
            _uiStatesController.InitializeOnScene();
            
            
            //Test
            Actor firstActor = _actorSpawner.ActorsOnScene[0];
            _scenePlayerController.SetControl(firstActor);
            _scenePlayerController.SetFollowTarget(firstActor);

        }

        private void InstantiateUI()
        {
            GameObject uiMain = new GameObject("[UI]");
            uiMain.transform.position = Vector3.zero;
            
            foreach (var uiState in _uiStatesController.States)
            {
                Instantiate(uiState, uiMain.transform, true);
            }
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
            _cameraHandler.OnSceneDispose();
            _uiStatesController.OnSceneDispose();
            _pauseNotifier.Clear();
        }
    }

}
