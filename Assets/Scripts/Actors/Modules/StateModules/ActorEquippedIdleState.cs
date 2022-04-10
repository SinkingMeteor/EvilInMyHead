using Sheldier.Actors.Data;
using Sheldier.Actors.Inventory;
using Sheldier.Common.Animation;
using Sheldier.Constants;

namespace Sheldier.Actors
{
    public class ActorEquippedIdleState : ActorDefaultIdleState
    {
        public override bool TransitionConditionIsDone => _inventoryModule.IsEquipped;
        public override int Priority => 1;

        private ActorsInventoryModule _inventoryModule;

        public ActorEquippedIdleState(ActorDynamicConfigData dynamicConfigData) : base(dynamicConfigData)
        {
        }
        
        public override void SetDependencies(ActorInternalData data)
        {
            base.SetDependencies(data);
            _inventoryModule = data.Actor.InventoryModule;
        }

        protected override void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationType.Idle_Equipped_Front,
                AnimationType.Idle_Equipped_Front_Side,
                AnimationType.Idle_Equipped_Back_Side,
                AnimationType.Idle_Equipped_Back,
            };
        }
    }
}