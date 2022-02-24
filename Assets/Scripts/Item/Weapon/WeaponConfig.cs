using Sheldier.Common.Animation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(menuName = "Sheldier/Items/WeaponConfig", fileName = "WeaponConfig")]
    public class WeaponConfig : ItemConfig
    {
        public ProjectileConfig ProjectileConfig => projectileConfig;
        public float Damage => damage;
        public float FireRate => fireRate;
        public int Capacity => capacity;
        public Vector2 AimLocalPosition => aimLocalPosition;
        public AnimationData WeaponBlowAnimation => weaponBlowAnimation;
        public AnimationData ReloadAnimation => reloadAnimation;
        public AmmoConfig RequiredAmmoType => requiredAmmoType;
        public override ItemGroup ItemGroup => ItemGroup.Weapon;

        [SerializeField] private float fireRate;
        [SerializeField] private float damage;
        [SerializeField] private int capacity;
        [SerializeField] private float reloadDuration;
        [SerializeField] private ProjectileConfig projectileConfig;
        [SerializeField] private AnimationData weaponBlowAnimation;
        [SerializeField] private AnimationData reloadAnimation;
        [SerializeField] private AmmoConfig requiredAmmoType;
        [SerializeField] private Vector2 aimLocalPosition;
        
        
        #if UNITY_EDITOR
        [BoxGroup("PreviewEditor")]
        [Range(0.2f, -0.2f)] public float CameraXPosition;
        [BoxGroup("PreviewEditor")]
        [Range(-0.2f, 0.2f)] public float CameraYPosition;
        [BoxGroup("PreviewEditor")]
        [Range(0.05f, 0.4f)] public float CameraSize;

#endif
    }
}