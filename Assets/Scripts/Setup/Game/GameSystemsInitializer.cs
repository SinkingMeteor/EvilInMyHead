using Sheldier.Actors.Builder;
using Sheldier.Actors.Inventory;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Factories;
using Sheldier.Graphs.DialogueSystem;
using UnityEngine;

namespace Sheldier.Setup
{
    public class GameSystemsInitializer
    {
        private InputProvider _inputProvider;
        private LocalizationProvider _localizationProvider;
        private AudioMixerController _audioMixerController;
        private ActorsEffectFactory _effectFactory;
        private ItemFactory _itemFactory;
        private ProjectilePool _projectilePool;
        private WeaponBlowPool _weaponBlowPool;
        private Inventory _inventory;
        private PathProvider _pathProvider;
        private CameraHandler _cameraHandler;
        private PauseNotifier _pauseNotifier;
        private InventorySlotPool _inventorySlotPool;
        private ActorBuilder _actorBuilder;
        private UIHintPool _uiHintPool;
        private InputBindHandler _inputBindHandler;
        private DialoguesProvider _dialoguesProvider;
        private SpeechCloudPool _speechCloudPool;
        private FontProvider _fontProvider;
        private ChoiceSlotPool _choiceSlotPool;
        private AssetProvider<Sprite> _spriteLoader;
        private AssetProvider<AnimationData> _animationLoader;
        private AssetProvider<ActorAnimationCollection> _animationCollectionLoader;
        private AssetProvider<AudioUnit> _audioLoader;
        private AssetProvider<TextAsset> _dataLoader;
        private AssetProvider<DialogueSystemGraph> _dialoguesLoader;

        public GameSystemsInitializer(InputProvider inputProvider,
            LocalizationProvider localizationProvider,
            AudioMixerController audioMixerController,
            ActorsEffectFactory effectFactory,
            ItemFactory itemFactory,
            ProjectilePool projectilePool,
            WeaponBlowPool weaponBlowPool,
            Inventory inventory,
            PathProvider pathProvider,
            CameraHandler cameraHandler,
            PauseNotifier pauseNotifier,
            InventorySlotPool inventorySlotPool,
            ActorBuilder actorBuilder,
            UIHintPool uiHintPool,
            InputBindHandler inputBindHandler,
            DialoguesProvider dialoguesProvider,
            SpeechCloudPool speechCloudPool,
            FontProvider fontProvider,
            ChoiceSlotPool choiceSlotPool,
            AssetProvider<Sprite> spriteLoader, 
            AssetProvider<AnimationData> animationLoader,
            AssetProvider<ActorAnimationCollection> animationCollectionLoader,
            AssetProvider<AudioUnit> audioLoader,
            AssetProvider<TextAsset> dataLoader,
            AssetProvider<DialogueSystemGraph> dialoguesLoader)
        {
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
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
        }

        public void InitializeSystems()
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
    }
}