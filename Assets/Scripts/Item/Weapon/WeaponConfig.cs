using System;
using Sheldier.Common.Animation;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Item
{
    [System.Serializable]
    public abstract class WeaponConfig : ItemConfig
    {
        public ProjectileConfig ProjectileConfig => _projectileConfig;
        public float Damage => _damage;
        public Vector2 AimLocalPosition => aimLocalPosition;
        public AnimationData WeaponBlowAnimation => _weaponBlowAnimation;
        public AnimationData RealodAnimation => _reloadAnimation;
        public override ItemGroup ItemGroup => ItemGroup.Weapon;

        [SerializeField] private float _damage;
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private AnimationData _weaponBlowAnimation;
        [SerializeField] private AnimationData _reloadAnimation;
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