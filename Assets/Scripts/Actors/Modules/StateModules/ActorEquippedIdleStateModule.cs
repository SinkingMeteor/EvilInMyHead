using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorEquippedIdleStateModule : ActorDefaultIdleState
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
                Animator.StringToHash("Idle_Equipped_Front"),
                Animator.StringToHash("Idle_Equipped_Front_Side"),
                Animator.StringToHash("Idle_Equipped_Back_Side"),
                Animator.StringToHash("Idle_Equipped_Back"),
            };
        }
    }
}