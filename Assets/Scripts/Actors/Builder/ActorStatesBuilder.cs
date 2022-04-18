using Sheldier.Actors.Data;
using Sheldier.Actors.Hand;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Factories;
using Sheldier.GameLocation;
using UnityEngine;

namespace Sheldier.Actors.Builder
{
    public class ActorStatesBuilder : ISubBuilder
    {
        private readonly Database<LocationDynamicConfig> _locationDynamicConfigDatabase;
        private readonly CurrentSceneDynamicData _currentSceneDynamicData;
        private readonly ActorDataFactory _actorDataFactory;
        private readonly ItemFactory _itemFactory;
        private ActorsHand _handTemplate;

        public ActorStatesBuilder(ActorDataFactory actorDataFactory, ItemFactory itemFactory,
            CurrentSceneDynamicData currentSceneDynamicData, Database<LocationDynamicConfig> locationDynamicConfigDatabase)
        {
            _currentSceneDynamicData = currentSceneDynamicData;
            _locationDynamicConfigDatabase = locationDynamicConfigDatabase;
            _itemFactory = itemFactory;
            _actorDataFactory = actorDataFactory;
            _handTemplate = Resources.Load<ActorsHand>(ResourcePaths.ACTOR_HAND);
        }

        public void Build(Actor actor, ActorStaticBuildData buildData)
        {
            bool canEquip = buildData.CanEquip;
            var locationData = _locationDynamicConfigDatabase.Get(_currentSceneDynamicData.CurrentLocationID);
            actor.StateModuleController.AddState(new ActorDefaultIdleState(locationData.Get(actor.Guid)));
            
            if (buildData.CanMove)
            {
                actor.StateModuleController.AddState(new ActorDefaultControlledMovementState(
                    _actorDataFactory.GetEntityNumericalStatCollection(actor.Guid),
                    _actorDataFactory.GetEntityStringStatCollection(actor.Guid),
                    _actorDataFactory.GetDynamicConfigData(actor.Guid)));
                actor.StateModuleController.AddState(new ActorFallState());
                actor.StateDataModule.Add(new StateData(GameplayConstants.FALL_STATE_DATA));                
            }
            if (canEquip)
            {
                AddHand(actor);
                actor.StateModuleController.AddState(new ActorEquippedIdleState(locationData.Get(actor.Guid)));
            }
            if(canEquip && buildData.CanMove)
                actor.StateModuleController.AddState(new ActorEquippedControlledMovementState(
                    _actorDataFactory.GetEntityNumericalStatCollection(actor.Guid),
                    _actorDataFactory.GetEntityStringStatCollection(actor.Guid),
                    _actorDataFactory.GetDynamicConfigData(actor.Guid)));

            if (buildData.CanJump)
            {
                actor.StateModuleController.AddState(new ActorJumpState());
                actor.StateDataModule.Add(new StateData(GameplayConstants.JUMP_STATE_DATA));                
            }

            if (buildData.CanMoveObjects)
            {
                actor.StateModuleController.AddState(new ActorMovesObjectsState(_actorDataFactory.GetEntityNumericalStatCollection(actor.Guid)));
                actor.StateDataModule.Add(new StateData(GameplayConstants.MOVES_OBJECTS_STATE_DATA));
            }
        }

        private void AddHand(Actor actor)
        {
            ActorsHand actorsHand = GameObject.Instantiate(_handTemplate, actor.ActorsView.transform, true);
            actorsHand.SetDependencies(_itemFactory);
            actor.AddExtraModule(actorsHand);
        }
    }
}