using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(fileName = "GunWeaponConfig", menuName = "Sheldier/Items/GunWeaponConfig")]

    public class GunWeaponConfig : WeaponConfig
    {
        public int Capacity => _capacity;
        public float FireRate => _fireRate;
        
        [HorizontalGroup("Data")][SerializeField] private int _capacity;
        [HorizontalGroup("Data")][SerializeField] private float _fireRate;
    }
}