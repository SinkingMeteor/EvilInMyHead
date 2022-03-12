using System;
using Sheldier.Actors.Data;
using Sheldier.Actors.Hand;
using Sheldier.Actors.Interact;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using Sheldier.Factories;
using UnityEngine;

namespace Sheldier.Actors.Builder
{
    public class ActorBuilder
    {
        private ScenePlayerController _scenePlayerController;
        private ActorsEffectFactory _effectFactory;
        private FixedTickHandler _fixedTickHandler;
        private PauseNotifier _pauseNotifier;
        private ISubBuilder[] _subBuilders;
        private TickHandler _tickHandler;
        private Actor _actorTemplate;

        public void Initialize()
        {
            _actorTemplate = Resources.Load<Actor>(ResourcePaths.ACTOR_TEMPLATE);
            _subBuilders = new ISubBuilder[]
            {
                new ActorStatesBuilder(),
                new ActorInteractBuilder(_scenePlayerController)
            };
        }
        
        public void SetDependencies(ActorsEffectFactory effectFactory, ScenePlayerController scenePlayerController, TickHandler tickHandler,
            FixedTickHandler fixedTickHandler, PauseNotifier pauseNotifier)
        {
            _scenePlayerController = scenePlayerController;
            _fixedTickHandler = fixedTickHandler;
            _pauseNotifier = pauseNotifier;
            _effectFactory = effectFactory;
            _tickHandler = tickHandler;
        }

        public Actor Build(ActorAnimationCollection appearance, ActorBuildData buildData, ActorData data)
        {
            Actor actor = GameObject.Instantiate(_actorTemplate);
            actor.SetDependencies(_tickHandler, _fixedTickHandler, _pauseNotifier);
            actor.ActorsView.SetActorAppearance(appearance);
            actor.Initialize();
            actor.DataModule.AddStaticData(data);
            
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
