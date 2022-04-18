using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Common.Utilities;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Item
{
    public class MoveObjectPoint : MonoBehaviour, IInteractReceiver
    {
        public Transform Transform => transform;
        public Vector2 ColliderPosition => transform.position.DiscardZ() + circleCollider.offset;
        public float ColliderSize => circleCollider.radius;

        [SerializeField] private CircleCollider2D circleCollider;
        
        public string ReceiverType => GameplayConstants.INTERACT_RECEIVER_MOVING_OBJECT;

        private bool _isMoving;

        public void OnEntered()
        {
        }

        public bool OnInteracted(Actor actor)
        {
            if (!actor.StateDataModule.IsItemExists(GameplayConstants.MOVES_OBJECTS_STATE_DATA))
                return false;
            _isMoving = !_isMoving; 
            actor.StateDataModule.Get(GameplayConstants.MOVES_OBJECTS_STATE_DATA).SetState(_isMoving);
            return false;
        }

        public void OnExit()
        {
        }
    }
}
