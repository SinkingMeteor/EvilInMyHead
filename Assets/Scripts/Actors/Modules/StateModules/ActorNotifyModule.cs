using System;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorNotifyModule
    {
        public event Action<SimpleItem> OnItemAddedToInventory;
        public event Action OnActorAttacks;
        public event Action OnActorReloads;
        public event Action OnSettedInput;
        
        
        public void NotifyAddedItemToInventory(SimpleItem pickUpObject) => OnItemAddedToInventory?.Invoke(pickUpObject);
        public void NotifyAttack() => OnActorAttacks?.Invoke();
        public void NotifySettedInput() => OnSettedInput?.Invoke();
        public void NotifyReloading() => OnActorReloads?.Invoke();
    }
}