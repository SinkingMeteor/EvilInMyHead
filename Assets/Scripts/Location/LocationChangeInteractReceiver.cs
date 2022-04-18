using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Common;
using Sheldier.Common.Utilities;
using Sheldier.Constants;
using UniRx;
using UnityEngine;

namespace Sheldier.GameLocation
{
    public class LocationChangeInteractReceiver : MonoBehaviour, IInteractReceiver
    {
        public Transform Transform => transform;
        public Vector2 ColliderPosition => transform.position.DiscardZ() + boxCollider.offset;
        public float ColliderSize => boxCollider.size.Min();
        public string ReceiverType => GameplayConstants.INTERACT_RECEIVER_LOCATION_CHANGER;

        [SerializeField] private DataReference locationReference;
        [SerializeField] private BoxCollider2D boxCollider;        
        public void OnEntered()
        {
            Debug.Log("EnteredToDoor");
        }

        public bool OnInteracted(Actor actor)
        {
            MessageBroker.Default.Publish(new ChangeLocationRequest() {LocationReference =  locationReference.Reference});
            return true;
        }

        public void OnExit()
        {
        }
    }
}