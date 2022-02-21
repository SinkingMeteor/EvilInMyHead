using System;
using Sheldier.Actors.Hand;
using Sheldier.Common.Animation;
using Sheldier.Constants;
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
        protected override ActorDirectionView GetDirectionView() => _actorTransformHandler.CalculateViewDirection();
        protected override void InitializeHashes()
        {
            _animationHashes = new[]
            {
                AnimationConstants.Animations[AnimationType.Run_Equipped_Front],
                AnimationConstants.Animations[AnimationType.Run_Equipped_Front_Side],
                AnimationConstants.Animations[AnimationType.Run_Equipped_Back_Side],
                AnimationConstants.Animations[AnimationType.Run_Equipped_Back],
            };
        }

        public override void Tick()
        {
            base.Tick();
            
            actorsHand.RotateHand(_inputController.CurrentInputProvider.CursorScreenDirection);
        }
    }
}