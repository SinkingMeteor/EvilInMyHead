using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class GameStartUp : MonoBehaviour
    {
        private GameSystemsInitializer _gameSystemsInitializer;
        private LoadingScreenProvider _loadingScreenProvider;

        private SceneLoadingOperation _sceneLoadingOperation;
        private SceneSetupOperation _sceneSetupOperation;
        private UILoadingOperation _uiLoadingOperation;

        private LocationStaticDataLoader _locationStaticDataLoader;
        private ActorStaticDataLoader _actorStaticDataLoader;
        private StatsStaticDataLoader _statsStaticDataLoader;
        private ItemStaticDataLoader _itemStaticDataLoader;
        private UIStaticDataLoader _uiStaticDataLoader;

        [Inject]
        private void InjectDependencies(LoadingScreenProvider loadingScreenProvider, 
                                        SceneLoadingOperation sceneLoadingOperation,
                                        UILoadingOperation uiLoadingOperation,
                                        SceneSetupOperation sceneSetupOperation,
                                        ActorStaticDataLoader actorStaticDataLoader, 
                                        ItemStaticDataLoader itemStaticDataLoader,
                                        UIStaticDataLoader uiStaticDataLoader,
                                        StatsStaticDataLoader statsStaticDataLoader,
                                        LocationStaticDataLoader locationStaticDataLoader,
                                        GameSystemsInitializer gameSystemsInitializer)
        {
            _gameSystemsInitializer = gameSystemsInitializer;
            _uiStaticDataLoader = uiStaticDataLoader;

            _locationStaticDataLoader = locationStaticDataLoader;
            _uiLoadingOperation = uiLoadingOperation;
            _sceneSetupOperation = sceneSetupOperation;
            _itemStaticDataLoader = itemStaticDataLoader;

            _statsStaticDataLoader = statsStaticDataLoader;
            _actorStaticDataLoader = actorStaticDataLoader;
            _sceneLoadingOperation = sceneLoadingOperation;
            _loadingScreenProvider = loadingScreenProvider;
        }
        private void Start()
        {
            InitializeSystems();
            LoadStaticData();
            LoadNextScene();
        }

        private void LoadStaticData()
        {
            _actorStaticDataLoader.LoadStaticData();
            _itemStaticDataLoader.LoadStaticData();
            _statsStaticDataLoader.LoadStaticData();
            _uiStaticDataLoader.LoadStaticData();
            _locationStaticDataLoader.LoadStaticData();
        }
        private void InitializeSystems()
        {
            _gameSystemsInitializer.InitializeSystems();
        }

        private void LoadNextScene()
        {
            ILoadOperation[] loadOperations =
            {
                _sceneLoadingOperation,
                _uiLoadingOperation,
                _sceneSetupOperation
            };
            
            new GameGlobalSettings().SetStarted();
#pragma warning disable 4014
            _loadingScreenProvider.LoadAndDestroy(loadOperations);
#pragma warning restore 4014
        }
    }
}
