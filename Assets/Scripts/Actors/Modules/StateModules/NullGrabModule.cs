using System;
using Sheldier.Item;

namespace Sheldier.Actors
{
    public class NullGrabModule : IGrabNotifier
    {
        public event Action<PickupObject> OnItemPickedUp;
    }
}