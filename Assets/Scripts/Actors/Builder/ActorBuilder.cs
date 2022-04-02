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
    public class ActorBuilder
    {
        private ScenePlayerController _scenePlayerController;
        private ActorsEffectFactory _effectFactory;
        private DialoguesProvider _dialoguesProvider;
        private FixedTickHandler _fixedTickHandler;
        private PauseNotifier _pauseNotifier;
        private ISubBuilder[] _subBuilders;
        private TickHandler _tickHandler;
        private Actor _actorTemplate;
        private ActorsMap _actorsMap;
        private Database<ActorStaticBuildData> _staticBuildDatabase;
        private ActorDataFactory _actorDataFactory;

        public void Initialize()
        {
            _actorTemplate = Resources.Load<Actor>(ResourcePaths.ACTOR_TEMPLATE);
            _subBuilders = new ISubBuilder[]
            {
                new ActorStatesBuilder(_actorDataFactory),
                new ActorInteractBuilder(_scenePlayerController, _dialoguesProvider, _actorDataFactory)
            };
        }
        
        public void SetDependencies(ActorsEffectFactory effectFactory, ScenePlayerController scenePlayerController, TickHandler tickHandler,
            FixedTickHandler fixedTickHandler, PauseNotifier pauseNotifier, ActorsMap actorsMap, DialoguesProvider dialoguesProvider, ActorDataFactory actorDataFactory)
        {
            _actorDataFactory = actorDataFactory;
            _actorsMap = actorsMap;
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
            ActorAnimationCollection actorAppearance = 
                ResourceLoader.Load<ActorAnimationCollection>(ResourcePaths.ACTOR_APPEARANCE_DIRECTORY + dynamicConfigData.ActorAppearance);
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

    public enum InteractType
    {
        None = 0,
        Replace = 1,
        Talk = 2
    }
    
}
