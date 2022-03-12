using Sheldier.Actors;
using Sheldier.Actors.Builder;
using Sheldier.Actors.Inventory;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using Sheldier.Factories;
using Sheldier.Installers;
using Sheldier.Item;
using Sheldier.UI;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        
        private LoadingScreenProvider _loadingScreenProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private ScenePlayerController _scenePlayerController;
        private LocalizationProvider _localizationProvider;
        private AudioMixerController _audioMixerController;
        private UILoadingOperation _uiLoadingOperation;
        private UIStatesController _uiStatesControler;
        private InventorySlotPool _inventorySlotPool;
        private InputBindHandler _inputBindHandler;
        private ActorsEffectFactory _effectFactory;
        private FixedTickHandler _fixedTickHandler;
        private LateTickHandler _lateTickHandler;
        private ProjectilePool _projectilePool;
        private WeaponBlowPool _weaponBlowPool;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private InputProvider _inputProvider;
        private ActorBuilder _actorBuilder;
        private PathProvider _pathProvider;
        private ActorSpawner _actorSpawner;
        private ItemFactory _itemFactory;
        private ItemSpawner _itemSpawner;
        private UIInstaller _uiInstaller;
        private TickHandler _tickHandler;
        private UIHintPool _uiHintPool;
        private Pathfinder _pathfinder;
        private Inventory _inventory;
        private ActorsMap _actorsMap;
        private ItemMap _itemMap;

        [Inject]
        private void InjectDependencies(LoadingScreenProvider loadingScreenProvider, InputProvider inputProvider, 
            SceneLoadingOperation sceneLoadingOperation, LocalizationProvider localizationProvider,
            AudioMixerController audioMixerController, ActorsEffectFactory effectFactory, ItemFactory itemFactory, ProjectilePool projectilePool,
            WeaponBlowPool weaponBlowPool, Inventory inventory, PathProvider pathProvider, UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler,
            PauseNotifier pauseNotifier, InventorySlotPool inventorySlotPool, ActorBuilder actorBuilder, UIHintPool uiHintPool, InputBindHandler inputBindHandler,
            Pathfinder pathfinder, TickHandler tickHandler, FixedTickHandler fixedTickHandler, LateTickHandler lateTickHandler, ItemMap itemMap,
            ActorsMap actorsMap, ScenePlayerController scenePlayerController, ItemSpawner itemSpawner, ActorSpawner actorSpawner,
            UIStatesController uiStatesControler, UIInstaller uiInstaller)
        {
            _itemMap = itemMap;
            _inventory = inventory;
            _actorsMap = actorsMap;
            _pathfinder = pathfinder;
            _uiHintPool = uiHintPool;
            _itemSpawner = itemSpawner;
            _tickHandler = tickHandler;
            _itemFactory = itemFactory;
            _uiInstaller = uiInstaller;
            _pathProvider = pathProvider;
            _actorSpawner = actorSpawner;
            _actorBuilder = actorBuilder;
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _effectFactory = effectFactory;
            _inputProvider = inputProvider;
            _weaponBlowPool = weaponBlowPool;
            _projectilePool = projectilePool;
            _lateTickHandler = lateTickHandler;
            _fixedTickHandler = fixedTickHandler;
            _inputBindHandler = inputBindHandler;
            _uiStatesControler = uiStatesControler;
            _inventorySlotPool = inventorySlotPool;
            _uiLoadingOperation = uiLoadingOperation;
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
            _sceneLoadingOperation = sceneLoadingOperation;
            _loadingScreenProvider = loadingScreenProvider;
            _scenePlayerController = scenePlayerController;
        }
        private void Start()
        {
            sceneContext.Run();

            _projectilePool.SetDependencies(_tickHandler);
            _projectilePool.Initialize();
            
            _weaponBlowPool.SetDependencies(_tickHandler);
            _weaponBlowPool.Initialize();
            
            _inventorySlotPool.Initialize();
            
            _uiHintPool.Initialize();
            
            _uiStatesControler.SetDependencies(_uiInstaller, _pauseNotifier, _inputProvider);
            
            _actorBuilder.SetDependencies(_effectFactory, _scenePlayerController, _tickHandler, _fixedTickHandler, _pauseNotifier);
            _actorBuilder.Initialize();
            
            _actorSpawner.SetDependencies(_actorBuilder);
            
            _pauseNotifier.Initialize();
            
            _inventory.Initialize();
            
            _itemFactory.SetDependencies(_itemMap, _projectilePool, _weaponBlowPool);
            _itemFactory.Initialize();
            
            _itemSpawner.SetDependencies(_itemFactory);
            
            _audioMixerController.Initialize();
            _localizationProvider.Initialize();
            _inputBindHandler.Initialize();
            _inputProvider.Initialize();
            _effectFactory.Initialize();
            
            _pathProvider.SetDependencies(_pathfinder);
            _pathProvider.Initialize();
            
            _cameraHandler.SetDependencies(_lateTickHandler, _inputProvider, _pauseNotifier);
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
