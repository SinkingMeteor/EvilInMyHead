using System;
using Sheldier.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors
{
    [RequireComponent(typeof(Collider2D))]
    public class ActorGrabModule : SerializedMonoBehaviour, IGrabNotifier
    {
        public event Action<PickupObject> OnItemPickedUp;

        public void Grab(PickupObject pickUpObject)
        {
            OnItemPickedUp?.Invoke(pickUpObject);
        }
    }
}