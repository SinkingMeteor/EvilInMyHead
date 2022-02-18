using UnityEngine;

namespace Sheldier.Item
{
    public interface IGunWeapon
    {
        WeaponConfig WeaponConfig { get; }
        
        void Shoot(Vector2 direction);

        void Reload();

    }
    
}