using Sheldier.Actors;
using UnityEngine;

namespace Sheldier.Item
{
    public class GunWeapon : IItem
    {
        public WeaponConfig WeaponConfig => _weaponConfig;
        public ItemConfig ItemConfig => _weaponConfig;

        private WeaponConfig _weaponConfig;

        public GunWeapon(WeaponConfig weaponConfig)
        {
            _weaponConfig = weaponConfig;
        }
        public void Shoot(Vector2 direction)
        {
            Debug.Log("Pistol shoots");
        }
        public void Reload()
        {
            Debug.Log("Pistol reloads");
        }
    }
}