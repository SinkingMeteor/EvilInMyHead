using Sheldier.Actors.Hand;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorEquippedIdleState : ActorDefaultIdleState
    {
        public override bool TransitionConditionIsDone => actorsHand.IsEquipped;
        public override int Priority => 1;

        [SerializeField] private ActorsHand actorsHand;

        public override void Tick()
        {
            base.Tick();
            actorsHand.RotateHand(_inputController.CurrentInputProvider.CursorScreenDirection.normalized);
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