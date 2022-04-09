using Sheldier.Actors;
using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Actors.Inventory;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Audio;
using Sheldier.Common.Cutscene;
using Sheldier.Common.Localization;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Factories;
using Sheldier.GameLocation;
using Sheldier.Graphs.DialogueSystem;
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
        private ActorStaticDataLoader _actorStaticDataLoader;
        private LocalizationProvider _localizationProvider;
        private ItemStaticDataLoader _itemStaticDataLoader;
        private AudioMixerController _audioMixerController;
        private SceneSetupOperation _sceneSetupOperation;
        private UILoadingOperation _uiLoadingOperation;
        private InventorySlotPool _inventorySlotPool;
        private DialoguesProvider _dialoguesProvider;
        private InputBindHandler _inputBindHandler;
        private ActorsEffectFactory _effectFactory;
        private SpeechCloudPool _speechCloudPool;
        private AssetProvider<AnimationData> _animationLoader;
        private ProjectilePool _projectilePool;
        private WeaponBlowPool _weaponBlowPool;
        private ChoiceSlotPool _choiceSlotPool;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private InputProvider _inputProvider;
        private ActorBuilder _actorBuilder;
        private PathProvider _pathProvider;
        private FontProvider _fontProvider;
        private AssetProvider<Sprite> _spriteLoader;
        private ItemFactory _itemFactory;
        private UIHintPool _uiHintPool;
        private Inventory _inventory;
        private AssetProvider<ActorAnimationCollection> _animationCollectionLoader;
        private AssetProvider<AudioUnit> _audioLoader;
        private AssetProvider<TextAsset> _dataLoader;
        private AssetProvider<DialogueSystemGraph> _dialoguesLoader;
        private UIStaticDataLoader _uiStaticDataLoader;

        
        [Inject]
        private void InjectDependencies(LoadingScreenProvider loadingScreenProvider, InputProvider inputProvider, 
            SceneLoadingOperation sceneLoadingOperation, LocalizationProvider localizationProvider,
            AudioMixerController audioMixerController, ActorsEffectFactory effectFactory, ItemFactory itemFactory, ProjectilePool projectilePool,
            WeaponBlowPool weaponBlowPool, Inventory inventory, PathProvider pathProvider, UILoadingOperation uiLoadingOperation, CameraHandler cameraHandler,
            PauseNotifier pauseNotifier, InventorySlotPool inventorySlotPool, ActorBuilder actorBuilder, UIHintPool uiHintPool, InputBindHandler inputBindHandler,
             DialoguesProvider dialoguesProvider, SpeechCloudPool speechCloudPool, 
             FontProvider fontProvider, ChoiceSlotPool choiceSlotPool,
            SceneSetupOperation sceneSetupOperation, ActorStaticDataLoader actorStaticDataLoader, 
            ItemStaticDataLoader itemStaticDataLoader,
            AssetProvider<Sprite> spriteLoader, AssetProvider<AnimationData> animationLoader, AssetProvider<ActorAnimationCollection> animationCollectionLoader,
            AssetProvider<AudioUnit> audioLoader, AssetProvider<TextAsset> dataLoader, AssetProvider<DialogueSystemGraph> dialoguesLoader, UIStaticDataLoader uiStaticDataLoader)
        {
            _uiStaticDataLoader = uiStaticDataLoader;
            _animationCollectionLoader = animationCollectionLoader;
            _dialoguesLoader = dialoguesLoader;
            _dataLoader = dataLoader;
            _audioLoader = audioLoader;
            _inventory = inventory;
            _uiHintPool = uiHintPool;
            _itemFactory = itemFactory;
            _fontProvider = fontProvider;
            _pathProvider = pathProvider;
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
            _inputBindHandler = inputBindHandler;
            _dialoguesProvider = dialoguesProvider;
            _inventorySlotPool = inventorySlotPool;
            _uiLoadingOperation = uiLoadingOperation;
            _sceneSetupOperation = sceneSetupOperation;
            _itemStaticDataLoader = itemStaticDataLoader;
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
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
            _uiStaticDataLoader.LoadStaticData();
        }
        private void InitializeSystems()
        {
            _audioLoader.Initialize();
            _dataLoader.Initialize();
            _dialoguesLoader.Initialize();
            _animationLoader.Initialize();
            _animationCollectionLoader.Initialize();
            _spriteLoader.Initialize();
            
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
