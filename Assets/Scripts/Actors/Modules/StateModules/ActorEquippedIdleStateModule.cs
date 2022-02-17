using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorEquippedIdleStateModule : ActorDefaultIdleState
    {
        public override bool TransitionConditionIsDone => actorsHand.IsEquipped;
        public override int Priority => 1;

        [SerializeField] private ActorsHand actorsHand;
        protected override void InitializeHashes()
        {
            _animationHashes = new[]
            {
                Animator.StringToHash("Idle_Equipped_Front"),
                Animator.StringToHash("Idle_Front_Equipped_Side"),
                Animator.StringToHash("Idle_Back_Equipped_Side"),
                Animator.StringToHash("Idle_Equipped_Back"),
            };
        }
    }
}