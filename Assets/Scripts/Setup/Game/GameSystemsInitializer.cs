using Sheldier.Actors.Builder;
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
using Sheldier.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

namespace Sheldier.Setup
{
    public class GameSystemsInitializer
    {
        private readonly InputProvider _inputProvider;
        private readonly LocalizationProvider _localizationProvider;
        private readonly AudioMixerController _audioMixerController;
        private readonly ActorsEffectFactory _effectFactory;
        private readonly ItemFactory _itemFactory;
        private readonly ProjectilePool _projectilePool;
        private readonly WeaponBlowPool _weaponBlowPool;
        private readonly Inventory _inventory;
        private readonly PathProvider _pathProvider;
        private readonly CameraHandler _cameraHandler;
        private readonly PauseNotifier _pauseNotifier;
        private readonly InventorySlotPool _inventorySlotPool;
        private readonly ActorBuilder _actorBuilder;
        private readonly UIHintPool _uiHintPool;
        private readonly InputBindHandler _inputBindHandler;
        private readonly SpeechCloudPool _speechCloudPool;
        private readonly FontProvider _fontProvider;
        private readonly ChoiceSlotPool _choiceSlotPool;
        private readonly InteractHintPool _interactHintPool;
        private readonly AssetProvider<Sprite> _spriteLoader;
        private readonly AssetProvider<AnimationData> _animationLoader;
        private readonly AssetProvider<ActorAnimationCollection> _animationCollectionLoader;
        private readonly AssetProvider<AudioUnit> _audioLoader;
        private readonly AssetProvider<TextAsset> _dataLoader;
        private readonly AssetProvider<DialogueSystemGraph> _dialoguesLoader;
        private readonly AssetProvider<VolumeProfile> _postProcessingLoader;
        private readonly ScenePlayerController _scenePlayerController;
        private readonly InteractHintController _interactHintController;
        private readonly CutsceneController _cutsceneController;
        private readonly AssetProvider<Cutscene> _cutsceneLoader;

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
            SpeechCloudPool speechCloudPool,
            InteractHintPool interactHintPool,
            FontProvider fontProvider,
            ChoiceSlotPool choiceSlotPool,
            InteractHintController interactHintController,
            AssetProvider<Sprite> spriteLoader, 
            AssetProvider<AnimationData> animationLoader,
            AssetProvider<ActorAnimationCollection> animationCollectionLoader,
            AssetProvider<AudioUnit> audioLoader,
            AssetProvider<TextAsset> dataLoader,
            AssetProvider<DialogueSystemGraph> dialoguesLoader,
            AssetProvider<VolumeProfile> postProcessingLoader,
            AssetProvider<Cutscene> cutsceneLoader,
            ScenePlayerController scenePlayerController,
            CutsceneController cutsceneController)
        {
            _cutsceneController = cutsceneController;
            _interactHintController = interactHintController;
            _scenePlayerController = scenePlayerController;
            _animationCollectionLoader = animationCollectionLoader;
            _dialoguesLoader = dialoguesLoader;
            _dataLoader = dataLoader;
            _audioLoader = audioLoader;
            _inventory = inventory;
            _uiHintPool = uiHintPool;
            _itemFactory = itemFactory;
            _interactHintPool = interactHintPool;
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
            _cutsceneLoader = cutsceneLoader;
            _projectilePool = projectilePool;
            _animationLoader = animationLoader;
            _speechCloudPool = speechCloudPool;
            _inputBindHandler = inputBindHandler;
            _inventorySlotPool = inventorySlotPool;
            _audioMixerController = audioMixerController;
            _localizationProvider = localizationProvider;
            _postProcessingLoader = postProcessingLoader;
        }

        public void InitializeSystems()
        {
            _audioLoader.Initialize();
            _dataLoader.Initialize();
            _dialoguesLoader.Initialize();
            _animationLoader.Initialize();
            _animationCollectionLoader.Initialize();
            _spriteLoader.Initialize();
            _postProcessingLoader.Initialize();
            _localizationProvider.Initialize();
            _fontProvider.Initialize();
            _projectilePool.Initialize();
            _weaponBlowPool.Initialize();
            _speechCloudPool.Initialize();
            _choiceSlotPool.Initialize();
            _interactHintPool.Initialize();
            _inventorySlotPool.Initialize();
            _interactHintController.Initialize();
            _uiHintPool.Initialize();
            _actorBuilder.Initialize();
            _pauseNotifier.Initialize();
            _inventory.Initialize();
            _cutsceneLoader.Initialize();
            _itemFactory.Initialize();
            _cutsceneController.Initialize();
            _audioMixerController.Initialize();
            _inputBindHandler.Initialize();
            _inputProvider.Initialize();
            _effectFactory.Initialize();
            _scenePlayerController.Initialize();
            _pathProvider.Initialize();
            _cameraHandler.Initialize();
        }

        public void DisposeSystems()
        {
            _cutsceneController.Dispose();
            _scenePlayerController.Dispose();
            _interactHintController.Dispose();
        }
    }
}