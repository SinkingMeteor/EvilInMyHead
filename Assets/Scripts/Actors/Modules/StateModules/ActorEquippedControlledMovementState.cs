using Sheldier.Actors.Data;
using Sheldier.Actors.Inventory;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorEquippedControlledMovementState : ActorDefaultControlledMovementState
    {
        public override bool TransitionConditionIsDone =>
            _inputController.CurrentInputProvider.MovementDirection.sqrMagnitude > Mathf.Epsilon &&
            _inventoryModule.IsEquipped;
        public override int Priority => 3;
        
        private ActorsInventoryModule _inventoryModule;
        
        public ActorEquippedControlledMovementState(DynamicNumericalEntityStatsCollection numericalStats, 
                                                    DynamicStringEntityStatsCollection stringStats,
                                                    ActorDynamicConfigData dynamicConfigData) : base(numericalStats, stringStats, dynamicConfigData)
        {
        }
        public override void SetDependencies(ActorInternalData data)
        {
            base.SetDependencies(data);
            _inventoryModule = data.Actor.InventoryModule;
        }

        protected override ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateViewDirection();
        protected override void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationType.Run_Equipped_Front,
                AnimationType.Run_Equipped_Front_Side,
                AnimationType.Run_Equipped_Back_Side,
                AnimationType.Run_Equipped_Back,
            };
        }
    }
}