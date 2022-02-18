using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(fileName = "GunWeaponConfig", menuName = "Sheldier/Items/GunWeaponConfig")]

    public class GunWeaponConfig : WeaponConfig
    {
        public int Capacity => _capacity;
        public float FireRate => _fireRate;
        
        [SerializeField] private int _capacity;
        [SerializeField] private float _fireRate;
    }
}