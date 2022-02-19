using Sheldier.Common;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Factories;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        
        private LoadingScreenProvider _loadingScreenProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private IInputProvider _inputProvider;
        private GameGlobalSettings _globalSettings;
        private LocalizationProvider _localizationProvider;
        private AudioMixerController _audioMixerController;
        private ActorsEffectFactory _effectFactory;
        private ItemFactory _itemFactory;

        [Inject]
        private void InjectDependencies(LoadingScreenProvider loadingScreenProvider, IInputProvider inputProvider, 
            SceneLoadingOperation sceneLoadingOperation, LocalizationProvider localizationProvider,
            AudioMixerController audioMixerController, ActorsEffectFactory effectFactory, ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _effectFactory = effectFactory;
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
            _sceneLoadingOperation = sceneLoadingOperation;
            _inputProvider = inputProvider;
            _loadingScreenProvider = loadingScreenProvider;
        }
        private void Start()
        {
            sceneContext.Run();
            _itemFactory.Initialize();
            _audioMixerController.Initialize();
            _localizationProvider.Initialize();
            _inputProvider.Initialize();
            _effectFactory.Initialize();
            ILoadOperation[] loadOperations =
            {
                _sceneLoadingOperation
            };
            
            new GameGlobalSettings().SetStarted();
            #pragma warning disable 4014
            _loadingScreenProvider.LoadAndDestroy(loadOperations);
            #pragma warning restore 4014
        }
    }
}
