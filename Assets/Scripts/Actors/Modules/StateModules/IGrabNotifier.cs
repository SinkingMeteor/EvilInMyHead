using System;
using Sheldier.Item;

namespace Sheldier.Actors
{
    public interface IGrabNotifier
    {
        event Action<PickupObject> OnItemPickedUp;
    }
}