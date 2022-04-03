using Sheldier.Actors;
using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Actors.Inventory;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Asyncs;
using Sheldier.Common.Audio;
using Sheldier.Common.Cutscene;
using Sheldier.Common.Localization;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Factories;
using Sheldier.GameLocation;
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

        private Database<ItemStaticProjectileData> _staticProjectileDatabase;
        private Database<ActorDynamicDialogueData> _dynamicDialogueDatabase;
        private SceneLocationController _sceneLocationController;
        private LoadingScreenProvider _loadingScreenProvider;
        private SceneLoadingOperation _sceneLoadingOperation;
        private ActorStaticDataLoader _actorStaticDataLoader;
        private ScenePlayerController _scenePlayerController;
        private LocalizationProvider _localizationProvider;
        private ItemStaticDataLoader _itemStaticDataLoader;
        private AudioMixerController _audioMixerController;
        private SceneSetupOperation _sceneSetupOperation;
        private UILoadingOperation _uiLoadingOperation;
        private UIStatesController _uiStatesController;
        private CutsceneController _cutsceneController;
        private InventorySlotPool _inventorySlotPool;
        private DialoguesProvider _dialoguesProvider;
        private InputBindHandler _inputBindHandler;
        private ActorsEffectFactory _effectFactory;
        private FixedTickHandler _fixedTickHandler;
        private ActorDataFactory _actorDataFactory;
        private LateTickHandler _lateTickHandler;
        private SpeechCloudPool _speechCloudPool;
        private AnimationLoader _animationLoader;
        private ProjectilePool _projectilePool;
        private WeaponBlowPool _weaponBlowPool;
        private ChoiceSlotPool _choiceSlotPool;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private InputProvider _inputProvider;
        private ActorBuilder _actorBuilder;
        private PathProvider _pathProvider;
        private ActorSpawner _actorSpawner;
        private FontProvider _fontProvider;
        private SpriteLoader _spriteLoader;
        private ISoundPlayer _soundPlayer;
        private ItemFactory _itemFactory;
        private ItemSpawner _itemSpawner;
        private UIInstaller _uiInstaller;
        private TickHandler _tickHandler;
        private UIHintPool _uiHintPool;
        private Pathfinder _pathfinder;
        private Inventory _inventory;
        private ItemMap _itemMap;
        private FontMap _fontMap;

        [Inject]
        private void InjectDependencies(LoadingScreenProvider loadingScreenProvider, InputProvider inputProvider, 
            SceneLoadingOperation sceneLoadingOperation, LocalizationProvider localizationProvider,
            AudioMixerController audioMixerController, ActorsEffectFactory effectFactory, ItemFactory itemFactory, ProjectilePool projectilePool,
            WeaponBlowPool weaponBlowPool, Inventory inventory, PathProvider pathProvider, UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler,
            PauseNotifier pauseNotifier, InventorySlotPool inventorySlotPool, ActorBuilder actorBuilder, UIHintPool uiHintPool, InputBindHandler inputBindHandler,
            Pathfinder pathfinder, TickHandler tickHandler, FixedTickHandler fixedTickHandler, LateTickHandler lateTickHandler, ItemMap itemMap,
            ScenePlayerController scenePlayerController, ItemSpawner itemSpawner, ActorSpawner actorSpawner,
            UIStatesController uiStatesController, UIInstaller uiInstaller, DialoguesProvider dialoguesProvider, SpeechCloudPool speechCloudPool, ISoundPlayer soundPlayer,
            CutsceneController cutsceneController, FontProvider fontProvider, FontMap fontMap, ChoiceSlotPool choiceSlotPool, SceneLocationController sceneLocationController,
            SceneSetupOperation sceneSetupOperation, ActorStaticDataLoader actorStaticDataLoader, ActorDataFactory actorDataFactory,
            Database<ActorDynamicDialogueData> dynamicDialogueDatabase, ItemStaticDataLoader itemStaticDataLoader, Database<ItemStaticProjectileData> staticProjectileDatabase,
            SpriteLoader spriteLoader, AnimationLoader animationLoader)
        {

            _itemMap = itemMap;
            _fontMap = fontMap;
            _inventory = inventory;
            _pathfinder = pathfinder;
            _uiHintPool = uiHintPool;
            _itemSpawner = itemSpawner;
            _tickHandler = tickHandler;
            _itemFactory = itemFactory;
            _uiInstaller = uiInstaller;
            _soundPlayer = soundPlayer;
            _fontProvider = fontProvider;
            _pathProvider = pathProvider;
            _actorSpawner = actorSpawner;
            _actorBuilder = actorBuilder;
            _spriteLoader = spriteLoader;
            _pauseNotifier = pauseNotifier;
            _cameraHandler = cameraHandler;
            _effectFactory = effectFactory;
            _inputProvider = inputProvider;
            _weaponBlowPool = weaponBlowPool;
            _choiceSlotPool = choiceSlotPool;
            _projectilePool = projectilePool;
            _animationLoader = animationLoader;
            _speechCloudPool = speechCloudPool;
            _lateTickHandler = lateTickHandler;
            _actorDataFactory = actorDataFactory;
            _fixedTickHandler = fixedTickHandler;
            _inputBindHandler = inputBindHandler;
            _dialoguesProvider = dialoguesProvider;
            _inventorySlotPool = inventorySlotPool;
            _uiStatesController = uiStatesController;
            _cutsceneController = cutsceneController;
            _uiLoadingOperation = uiLoadingOperation;
            _sceneSetupOperation = sceneSetupOperation;
            _itemStaticDataLoader = itemStaticDataLoader;
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
            _actorStaticDataLoader = actorStaticDataLoader;
            _sceneLoadingOperation = sceneLoadingOperation;
            _loadingScreenProvider = loadingScreenProvider;
            _scenePlayerController = scenePlayerController;
            _dynamicDialogueDatabase = dynamicDialogueDatabase;
            _sceneLocationController = sceneLocationController;
            _staticProjectileDatabase = staticProjectileDatabase;
        }
        private void Start()
        {
            sceneContext.Run();

            LoadStaticData();
            SetDependenciesToSystems();
            InitializeSystems();
            LoadNextScene();
        }

        private void LoadStaticData()
        {
            _actorStaticDataLoader.LoadStaticData();
            _itemStaticDataLoader.LoadStaticData();
        }
        private void SetDependenciesToSystems()
        {
            _fontProvider.SetDependencies(_localizationProvider, _fontMap);
            _projectilePool.SetDependencies(_tickHandler, _staticProjectileDatabase, _spriteLoader);
            _weaponBlowPool.SetDependencies(_tickHandler, _animationLoader);
            _speechCloudPool.SetDependencies(_soundPlayer, _fontProvider, _dynamicDialogueDatabase);
            _choiceSlotPool.SetDependencies(_fontProvider);
            _uiHintPool.SetDependencies(_fontProvider);
            _uiStatesController.SetDependencies(_uiInstaller, _pauseNotifier, _inputProvider);
            _dialoguesProvider.SetDependencies(_actorSpawner);
            _actorBuilder.SetDependencies(_effectFactory, _scenePlayerController, _tickHandler, _fixedTickHandler, _pauseNotifier, _dialoguesProvider, _actorDataFactory);
            _actorSpawner.SetDependencies(_actorBuilder);
            _itemSpawner.SetDependencies(_itemFactory);
            _cutsceneController.SetDependencies(_actorSpawner, _pauseNotifier, _pathProvider, _scenePlayerController, _dialoguesProvider);
            _pathProvider.SetDependencies(_pathfinder);
            _cameraHandler.SetDependencies(_lateTickHandler, _inputProvider, _pauseNotifier);
            _sceneLocationController.SetDependencies(_itemSpawner, _actorSpawner, _pathfinder);
            new AsyncWaitersFactory().SetDependencies(_tickHandler);
        }
        private void InitializeSystems()
        {
            _localizationProvider.Initialize();
            _fontProvider.Initialize();
            _projectilePool.Initialize();
            _weaponBlowPool.Initialize();
            _speechCloudPool.Initialize();
            _choiceSlotPool.Initialize();
            _inventorySlotPool.Initialize();
            _uiHintPool.Initialize();
            _dialoguesProvider.Initialize();
            _actorBuilder.Initialize();
            _pauseNotifier.Initialize();
            _inventory.Initialize();
            _itemFactory.Initialize();
            _audioMixerController.Initialize();
            _inputBindHandler.Initialize();
            _inputProvider.Initialize();
            _effectFactory.Initialize();
            _pathProvider.Initialize();
            _cameraHandler.Initialize();
        }

        private void LoadNextScene()
        {
            ILoadOperation[] loadOperations =
            {
                _uiLoadingOperation,
                _sceneLoadingOperation,
                _sceneSetupOperation
            };
            
            new GameGlobalSettings().SetStarted();
#pragma warning disable 4014
            _loadingScreenProvider.LoadAndDestroy(loadOperations);
#pragma warning restore 4014
        }
    }
}
