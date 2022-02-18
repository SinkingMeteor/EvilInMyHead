using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(fileName = "MeleeWeaponConfig", menuName = "Sheldier/Items/MeleeWeaponConfig")]
    public class MeleeWeaponConfig : WeaponConfig
    {
        public float HitDistance => _hitDistance;
        
        private float _hitDistance;
    }
}