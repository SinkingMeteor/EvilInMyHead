using Sheldier.Actors.Data;
using Sheldier.Actors.Hand;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using Sheldier.Factories;
using Sheldier.Installers;
using UnityEngine;
using Zenject;

namespace Sheldier.Actors.Builder
{
    public class ActorBuilder
    {
        private Actor _actorTemplate;
        private ISubBuilder[] _subBuilders;
        private ActorsInstaller _actorsInstaller;
        private ActorsMap _actorsMap;
        private ActorsEffectFactory _effectFactory;

        public void Initialize()
        {
            _actorTemplate = Resources.Load<Actor>(ResourcePaths.ACTOR_TEMPLATE);
            _subBuilders = new[]
            {
                new ActorStatesBuilder()
            };
        }

        [Inject]
        private void InjectDependencies(ActorsInstaller actorsInstaller, ActorsMap actorsMap, ActorsEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _actorsMap = actorsMap;
            _actorsInstaller = actorsInstaller;
        }

        public Actor Build(ActorAnimationCollection appearance)
        {
            Actor actor = GameObject.Instantiate(_actorTemplate);
            ActorBuildData buildData = _actorsMap.Actors[appearance];
            _actorsInstaller.InjectActor(actor);
            actor.ActorsView.SetActorAppearance(appearance);
            actor.Initialize();
            actor.DataModule.AddStaticData(buildData.Data);
            
            if(buildData.IsEffectsPerceptive)
                actor.AddExtraModule(new ActorEffectModule(_effectFactory));
            
            foreach (var subBuilder in _subBuilders)
            {
                subBuilder.Build(actor, buildData);
            }
            return actor;
        }

        private class ActorStatesBuilder : ISubBuilder
        {
            private ActorsHand _handTemplate;
            public ActorStatesBuilder()
            {
                _handTemplate = Resources.Load<ActorsHand>(ResourcePaths.ACTOR_HAND);
            }

            public void Build(Actor actor, ActorBuildData buildData)
            {
                bool canEquip = buildData.CanEquip;
                actor.StateModuleController.AddState(new ActorDefaultIdleState());
                if(buildData.CanMove)
                    actor.StateModuleController.AddState(new ActorDefaultControlledMovementState());
                if (canEquip)
                {
                    AddHand(actor);
                    actor.StateModuleController.AddState(new ActorEquippedIdleState());
                }
                if(canEquip && buildData.CanMove)
                    actor.StateModuleController.AddState(new ActorEquippedControlledMovementState());
            }

            private void AddHand(Actor actor)
            {
                ActorsHand actorsHand = GameObject.Instantiate(_handTemplate, actor.ActorsView.transform, true);
                actor.AddExtraModule(actorsHand);
            }
        }
        private interface ISubBuilder
        {
            void Build(Actor actor, ActorBuildData buildData);
        }
    }

    
}
