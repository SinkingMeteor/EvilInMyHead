using System;
using Sheldier.Actors.Data;
using Sheldier.Actors.Hand;
using Sheldier.Actors.Interact;
using Sheldier.Common;
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
        private ScenePlayerController _scenePlayerController;

        public void Initialize()
        {
            _actorTemplate = Resources.Load<Actor>(ResourcePaths.ACTOR_TEMPLATE);
            _subBuilders = new ISubBuilder[]
            {
                new ActorStatesBuilder(),
                new ActorInteractBuilder(_scenePlayerController)
            };
        }

        [Inject]
        private void InjectDependencies(ActorsInstaller actorsInstaller, ActorsMap actorsMap,
            ActorsEffectFactory effectFactory, ScenePlayerController scenePlayerController)
        {
            _scenePlayerController = scenePlayerController;
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
            if(buildData.CanAttack)
                actor.AddExtraModule(new ActorAttackModule());
            
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
        private class ActorInteractBuilder : ISubBuilder
        {
            private readonly GameObject _interactBase;
            private readonly ScenePlayerController _scenePlayerController;
            private Material _interactMaterial;

            public ActorInteractBuilder(ScenePlayerController scenePlayerController)
            {
                _scenePlayerController = scenePlayerController;
                _interactBase = Resources.Load<GameObject>(ResourcePaths.ACTOR_INTERACT_MODULE);
                _interactMaterial = Resources.Load<Material>(ResourcePaths.UNLIT_OUTLINE_MATERIAL);
            }
            public void Build(Actor actor, ActorBuildData buildData)
            {
                if (!buildData.CanInteract && buildData.InteractType == InteractType.None) return;
                
                   GameObject body = GameObject.Instantiate(_interactBase, actor.transform, true);

                   if (buildData.CanInteract)
                   {
                       var interactNotifier = body.AddComponent<ActorsInteractNotifier>();
                       actor.AddExtraModule(interactNotifier);
                   }

                   if (buildData.InteractType == InteractType.None) return;

                   actor.AddExtraModule(CreateInteractReceiver(body, actor, buildData.InteractType));
            }

            private IExtraActorModule CreateInteractReceiver(GameObject body, Actor actor, InteractType interactType)
            {
                return interactType switch
                {
                    InteractType.Replace => CreateReplaceReceiver(body, actor),
                    InteractType.Talk => throw new NotImplementedException(),
                    InteractType.None => throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null),
                    _ => throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null)
                };
            }

            private IExtraActorModule CreateReplaceReceiver(GameObject body, Actor actor)
            {
                ReplaceInteractReceiver receiver = body.AddComponent<ReplaceInteractReceiver>();
                receiver.SetDependencies(_scenePlayerController, _interactMaterial);
                return receiver;
            }
            
        }
        private interface ISubBuilder
        {
            void Build(Actor actor, ActorBuildData buildData);
        }
    }

    public enum InteractType
    {
        None,
        Replace,
        Talk
    }
    
}
