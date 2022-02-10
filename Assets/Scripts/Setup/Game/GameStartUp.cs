using UnityEngine;
using UnityEngine.SceneManagement;
using Sheldier.Constants;
using Zenject;

namespace Sheldier.Setup
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        private LoadingScreenProvider _loadingScreenProvider;

        [Inject]
        private void GetDependencies(LoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }
        private void Start()
        {
            //TODO: Load Preferences
            sceneContext.Run();
            ILoadOperation[] loadOperations =
            {
                new SceneLoadingOperation(new []{SceneNames.COLONY_OUTSIDE})
            };
            _loadingScreenProvider.LoadAndDestroy(loadOperations);
        }
    }
}
