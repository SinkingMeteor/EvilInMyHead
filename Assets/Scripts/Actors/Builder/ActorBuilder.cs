using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Factories;
using UnityEngine;

namespace Sheldier.Actors.Builder
{
    public class ActorBuilder : IActorBuilder
    {
        private readonly AssetProvider<ActorAnimationCollection> _appearanceLoader;
        private readonly ScenePlayerController _scenePlayerController;
        private readonly DialoguesProvider _dialoguesProvider;
        private readonly ActorsEffectFactory _effectFactory;
        private readonly ActorDataFactory _actorDataFactory;
        private readonly FixedTickHandler _fixedTickHandler;
        private readonly ICameraFollower _cameraFollower;
        private readonly PauseNotifier _pauseNotifier;
        private readonly TickHandler _tickHandler;
        private readonly ItemFactory _itemFactory;
        
        private ISubBuilder[] _subBuilders;
        private Actor _actorTemplate;

        public ActorBuilder(ActorsEffectFactory effectFactory,
                            ScenePlayerController scenePlayerController,
                            TickHandler tickHandler,
                            FixedTickHandler fixedTickHandler,
                            PauseNotifier pauseNotifier,
                            DialoguesProvider dialoguesProvider,
                            ActorDataFactory actorDataFactory,
                            ItemFactory itemFactory,
                            AssetProvider<ActorAnimationCollection> appearanceLoader,
                            ICameraFollower cameraFollower)
        {
            _cameraFollower = cameraFollower;
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
        
        public void Initialize()
        {
            _actorTemplate = Resources.Load<Actor>(ResourcePaths.ACTOR_TEMPLATE);
            _subBuilders = new ISubBuilder[]
            {
                new ActorStatesBuilder(_actorDataFactory, _itemFactory, _cameraFollower),
                new ActorInteractBuilder(_scenePlayerController, _dialoguesProvider, _cameraFollower)
            };
        }

        public Actor Build(string typeID, string guid, ActorPlaceholder actorPlaceholder)
        {
            bool isNewActor = !_actorDataFactory.IsDynamicConfigExists(guid);
            var staticConfig = _actorDataFactory.GetStaticConfigData(typeID);
            ActorStaticBuildData buildData = _actorDataFactory.GetBuildData(typeID);
            ActorDynamicConfigData dynamicConfigData = _actorDataFactory.GetDynamicActorConfig(typeID, guid);
            ActorAnimationCollection actorAppearance = _appearanceLoader.Get(staticConfig.ActorAppearance);
            

            Actor actor = GameObject.Instantiate(_actorTemplate);
            actor.SetDependencies(dynamicConfigData.Guid, _tickHandler, _fixedTickHandler, _pauseNotifier);
            actor.ActorsView.SetActorAppearance(actorAppearance);
            actor.Initialize();

            actor.StateDataModule.Add(new StateData(GameplayConstants.DOES_ANY_STATE_DATA));
            
            if (buildData.IsEffectPerceptive)
            {
                var effectData = _actorDataFactory.CreateDynamicEffectData(dynamicConfigData.Guid);
                actor.AddExtraModule(new ActorEffectModule(effectData, _effectFactory));
            }

            if (buildData.CanAttack)
                actor.AddExtraModule(new ActorAttackModule());

            foreach (var subBuilder in _subBuilders)
            {
                subBuilder.Build(actor, buildData);
            }
            actor.transform.position = isNewActor ? actorPlaceholder.transform.position : dynamicConfigData.Position;
            return actor;
        }
    }
}
