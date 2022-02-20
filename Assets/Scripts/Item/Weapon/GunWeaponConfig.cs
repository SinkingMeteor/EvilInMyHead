using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "GunWeaponConfig", menuName = "Sheldier/Items/GunWeaponConfig")]
    public class GunWeaponConfig : WeaponConfig
    {
        public int Capacity => _capacity;
        public float FireRate => _fireRate;
        
        [PropertyOrder(9)][HorizontalGroup("Data")][SerializeField] private int _capacity;
        [PropertyOrder(9)][HorizontalGroup("Data")][SerializeField] private float _fireRate;

    }
}