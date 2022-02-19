using System;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorNotifyModule
    {
        public event Action<ItemConfig> OnItemPickedUp;
        public event Action<Vector2> OnActorAttacks;
        public event Action OnActorReloads;
        public event Action<GunWeapon> OnWeaponPickedUp;
        public event Action<GunWeapon> OnWeaponDropped;
        
        
        
        public void NotifyPickUpItem(ItemConfig pickUpObject) => OnItemPickedUp?.Invoke(pickUpObject);
        public void NotifyAttack(Vector2 direction) => OnActorAttacks?.Invoke(direction);
        public void NotifyReloading() => OnActorReloads?.Invoke();
        public void NotifyWeaponPickUp(GunWeapon weapon) => OnWeaponPickedUp?.Invoke(weapon);
        public void NotifyWeaponDropped(GunWeapon weapon) => OnWeaponPickedUp?.Invoke(weapon);
    }
}