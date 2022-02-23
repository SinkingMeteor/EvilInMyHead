using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class WeaponedHandInputReceiver : IHandInputReceiver
    {
        private GunWeapon _currentWeapon;
        
        private readonly ActorNotifyModule _notifier;
        private readonly ActorInputController _actorInputController;

        public WeaponedHandInputReceiver(GunWeapon weapon, ActorNotifyModule notifier, HandView itemView)
        {
            _currentWeapon = weapon;
            _notifier = notifier;
           // weapon.SetWeaponView(itemView);
            notifier.OnActorAttacks += AttackByWeapon;
            notifier.OnActorReloads += ReloadWeapon;
        }
        private void ReloadWeapon() => _currentWeapon.Reload();
        private void AttackByWeapon(Vector2 dir)
        {
            _currentWeapon.Shoot(dir);
        }

        public float GetHandRotation(float angle)
        {
            return _currentWeapon.GetHandRotation(angle);
        }
        public void Dispose()
        {
            _currentWeapon.Unequip();
            _notifier.OnActorAttacks += AttackByWeapon;
            _notifier.OnActorReloads += ReloadWeapon;
        }
    }
}