using Sheldier.Actors.Builder;
using Sheldier.Actors.Inventory;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using Sheldier.Factories;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        
        private LoadingScreenProvider _loadingScreenProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private InputProvider _inputProvider;
        private LocalizationProvider _localizationProvider;
        private AudioMixerController _audioMixerController;
        private ActorsEffectFactory _effectFactory;
        private ItemFactory _itemFactory;
        private ProjectilePool _projectilePool;
        private WeaponBlowPool _weaponBlowPool;
        private Inventory _inventory;
        private PathProvider _pathProvider;
        private UILoadingOperation _uiLoadingOperation;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private InventorySlotPool _inventorySlotPool;
        private ActorBuilder _actorBuilder;
        private UIHintPool _uiHintPool;
        private InputBindHandler _inputBindHandler;

        [Inject]
        private void InjectDependencies(LoadingScreenProvider loadingScreenProvider, InputProvider inputProvider, 
            SceneLoadingOperation sceneLoadingOperation, LocalizationProvider localizationProvider,
            AudioMixerController audioMixerController, ActorsEffectFactory effectFactory, ItemFactory itemFactory, ProjectilePool projectilePool,
            WeaponBlowPool weaponBlowPool, Inventory inventory, PathProvider pathProvider, UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler,
            PauseNotifier pauseNotifier, InventorySlotPool inventorySlotPool, ActorBuilder actorBuilder, UIHintPool uiHintPool, InputBindHandler inputBindHandler)
        {
            _inputBindHandler = inputBindHandler;
            _uiHintPool = uiHintPool;
            _inventorySlotPool = inventorySlotPool;
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _pathProvider = pathProvider;
            _inventory = inventory;
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
            _itemFactory = itemFactory;
            _effectFactory = effectFactory;
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
            _sceneLoadingOperation = sceneLoadingOperation;
            _inputProvider = inputProvider;
            _loadingScreenProvider = loadingScreenProvider;
            _uiLoadingOperation = uiLoadingOperation;
            _actorBuilder = actorBuilder;
        }
        private void Start()
        {
            sceneContext.Run();

            _projectilePool.Initialize();
            _weaponBlowPool.Initialize();
            _inventorySlotPool.Initialize();
            _uiHintPool.Initialize();
            
            _actorBuilder.Initialize();
            
            _pauseNotifier.Initialize();
            
            _inventory.Initialize();
            _itemFactory.Initialize();
            _audioMixerController.Initialize();
            _localizationProvider.Initialize();
            _inputBindHandler.Initialize();
            _inputProvider.Initialize();
            _effectFactory.Initialize();
            _pathProvider.Initialize();
            _cameraHandler.Initialize();

            ILoadOperation[] loadOperations =
            {
                _uiLoadingOperation,
                _sceneLoadingOperation,
            };
            
            new GameGlobalSettings().SetStarted();
            #pragma warning disable 4014
            _loadingScreenProvider.LoadAndDestroy(loadOperations);
            #pragma warning restore 4014
        }
    }
}
