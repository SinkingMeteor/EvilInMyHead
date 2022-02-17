using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorEquippedControlledMovementState : ActorDefaultControlledMovementState
    {
        public override bool TransitionConditionIsDone =>
            _inputController.CurrentInputProvider.MovementDirection.sqrMagnitude > Mathf.Epsilon &&
            actorsHand.IsEquipped;
        public override int Priority => 3;

        [SerializeField] private ActorsHand actorsHand;

        protected override void InitializeHashes()
        {
            _animationHashes = new[]
            {
                Animator.StringToHash("Run_Equipped_Front"),
                Animator.StringToHash("Run_Front_Equipped_Side"),
                Animator.StringToHash("Run_Back_Equipped_Side"),
                Animator.StringToHash("Run_Equipped_Back"),
            };
        }
    }
}