using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Common;
using UniRx;
using UnityEngine;

namespace Sheldier.GameLocation
{
    public class LocationChangeInteractReceiver : MonoBehaviour, IInteractReceiver
    {
        public Transform Transform => transform;

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