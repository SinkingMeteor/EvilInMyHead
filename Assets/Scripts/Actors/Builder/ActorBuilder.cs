using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Factories;
using UnityEngine;
using Zenject;

namespace Sheldier.Actors.Builder
{
    public class ActorBuilder
    {
        private AssetProvider<ActorAnimationCollection> _appearanceLoader;
        private ScenePlayerController _scenePlayerController;
        private ActorsEffectFactory _effectFactory;
        private DialoguesProvider _dialoguesProvider;
        private FixedTickHandler _fixedTickHandler;
        private PauseNotifier _pauseNotifier;
        private ISubBuilder[] _subBuilders;
        private TickHandler _tickHandler;
        private Actor _actorTemplate;
        private ActorDataFactory _actorDataFactory;
        private ItemFactory _itemFactory;

        public void Initialize()
        {
            _actorTemplate = Resources.Load<Actor>(ResourcePaths.ACTOR_TEMPLATE);
            _subBuilders = new ISubBuilder[]
            {
                new ActorStatesBuilder(_actorDataFactory, _itemFactory),
                new ActorInteractBuilder(_scenePlayerController, _dialoguesProvider, _actorDataFactory)
            };
        }
        
        [Inject]
        private void InjectDependencies(ActorsEffectFactory effectFactory, ScenePlayerController scenePlayerController, TickHandler tickHandler,
            FixedTickHandler fixedTickHandler, PauseNotifier pauseNotifier, DialoguesProvider dialoguesProvider, ActorDataFactory actorDataFactory,
            ItemFactory itemFactory, AssetProvider<ActorAnimationCollection> appearanceLoader)
        {
            _appearanceLoader = appearanceLoader;
            _itemFactory = itemFactory;
            _actorDataFactory = actorDataFactory;
            _dialoguesProvider = dialoguesProvider;
            _scenePlayerController = scenePlayerController;
            _fixedTickHandler = fixedTickHandler;
            _pauseNotifier = pauseNotifier;
            _effectFactory = effectFactory;
            _tickHandler = tickHandler;
        }

        public Actor Build(string typeID)
        {
            ActorStaticBuildData buildData = _actorDataFactory.GetBuildData(typeID);
            ActorDynamicConfigData dynamicConfigData = _actorDataFactory.CreateDynamicActorConfig(typeID);
            ActorAnimationCollection actorAppearance = _appearanceLoader.Get(dynamicConfigData.ActorAppearance);
            _actorDataFactory.CreateDynamicDialogueData(dynamicConfigData.Guid);
            
            
            Actor actor = GameObject.Instantiate(_actorTemplate);
            actor.SetDependencies(dynamicConfigData, _tickHandler, _fixedTickHandler, _pauseNotifier);
            actor.ActorsView.SetActorAppearance(actorAppearance);
            actor.Initialize();

            if (buildData.IsEffectPerceptive)
            {
                var effectData = _actorDataFactory.CreateDynamicEffectData(dynamicConfigData.Guid);
                actor.AddExtraModule(new ActorEffectModule(effectData, _effectFactory));
            }
            if(buildData.CanAttack)
                actor.AddExtraModule(new ActorAttackModule());
            
            foreach (var subBuilder in _subBuilders)
            {
                subBuilder.Build(actor, buildData);
            }
            return actor;
        }
    }
}
