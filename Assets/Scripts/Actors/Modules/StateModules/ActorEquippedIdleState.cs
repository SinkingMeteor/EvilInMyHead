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

        public override void SetDependencies(ActorInternalData data)
        {
            base.SetDependencies(data);
            _inventoryModule = data.Actor.InventoryModule;
        }

        protected override void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationConstants.Animations[AnimationType.Idle_Equipped_Front],
                AnimationConstants.Animations[AnimationType.Idle_Equipped_Front_Side],
                AnimationConstants.Animations[AnimationType.Idle_Equipped_Back_Side],
                AnimationConstants.Animations[AnimationType.Idle_Equipped_Back],
            };
        }
    }
}