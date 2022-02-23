using System;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorNotifyModule
    {
        public event Action<SimpleItem> OnItemPickedUp;
        public event Action<Vector2> OnActorAttacks;
        public event Action OnActorReloads;
        public event Action<GunWeapon> OnWeaponPickedUp;
        public event Action<GunWeapon> OnWeaponDropped;
        public event Action OnSettedInput;
        
        
        public void NotifyPickUpItem(SimpleItem pickUpObject) => OnItemPickedUp?.Invoke(pickUpObject);
        public void NotifyAttack(Vector2 direction) => OnActorAttacks?.Invoke(direction);
        public void NotifySettedInput() => OnSettedInput?.Invoke();
        public void NotifyReloading() => OnActorReloads?.Invoke();
        public void NotifyWeaponPickUp(GunWeapon weapon) => OnWeaponPickedUp?.Invoke(weapon);
        public void NotifyWeaponDropped(GunWeapon weapon) => OnWeaponPickedUp?.Invoke(weapon);
    }
}