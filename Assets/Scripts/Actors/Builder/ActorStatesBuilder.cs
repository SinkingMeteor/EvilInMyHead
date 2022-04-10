using Sheldier.Actors.Data;
using Sheldier.Actors.Hand;
using Sheldier.Constants;
using Sheldier.Factories;
using UnityEngine;

namespace Sheldier.Actors.Builder
{
    public class ActorStatesBuilder : ISubBuilder
    {
        private readonly ActorDataFactory _actorDataFactory;
        private readonly ItemFactory _itemFactory;
        private ActorsHand _handTemplate;

        public ActorStatesBuilder(ActorDataFactory actorDataFactory, ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _actorDataFactory = actorDataFactory;
            _handTemplate = Resources.Load<ActorsHand>(ResourcePaths.ACTOR_HAND);
        }

        public void Build(Actor actor, ActorStaticBuildData buildData)
        {
            bool canEquip = buildData.CanEquip;
            actor.StateModuleController.AddState(new ActorDefaultIdleState(_actorDataFactory.GetDynamicConfigData(actor.Guid)));
            
            if (buildData.CanMove)
            {
                
                actor.StateModuleController.AddState(new ActorDefaultControlledMovementState(
                    _actorDataFactory.GetEntityNumericalStatCollection(actor.Guid),
                    _actorDataFactory.GetEntityStringStatCollection(actor.Guid),
                    _actorDataFactory.GetDynamicConfigData(actor.Guid)));
                actor.StateModuleController.AddState(new ActorFallState());
            }
            if (canEquip)
            {
                AddHand(actor);
                actor.StateModuleController.AddState(new ActorEquippedIdleState(_actorDataFactory.GetDynamicConfigData(actor.Guid)));
            }
            if(canEquip && buildData.CanMove)
                actor.StateModuleController.AddState(new ActorEquippedControlledMovementState(
                    _actorDataFactory.GetEntityNumericalStatCollection(actor.Guid),
                    _actorDataFactory.GetEntityStringStatCollection(actor.Guid),
                    _actorDataFactory.GetDynamicConfigData(actor.Guid)));
                
            if(buildData.CanJump)
                actor.StateModuleController.AddState(new ActorJumpState());
        }

        private void AddHand(Actor actor)
        {
            ActorsHand actorsHand = GameObject.Instantiate(_handTemplate, actor.ActorsView.transform, true);
            actorsHand.SetDependencies(_itemFactory);
            actor.AddExtraModule(actorsHand);
        }
    }
}