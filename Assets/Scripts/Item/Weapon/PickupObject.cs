using Sheldier.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    public abstract class PickupObject : SerializedMonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.TryGetComponent(out ActorGrabModule grabModule))
                grabModule.Grab(this);
        }
    }
}