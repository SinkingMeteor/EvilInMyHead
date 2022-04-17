using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Common;
using Sheldier.Constants;
using UniRx;
using UnityEngine;

namespace Sheldier.GameLocation
{
    public class LocationChangeInteractReceiver : MonoBehaviour, IInteractReceiver
    {
        public Transform Transform => transform;
        public string ReceiverType => GameplayConstants.INTERACT_RECEIVER_LOCATION_CHANGER;

        [SerializeField] private DataReference locationReference;

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