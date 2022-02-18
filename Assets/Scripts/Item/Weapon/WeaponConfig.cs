using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Item
{
    public abstract class WeaponConfig : ItemConfig
    {
        public Sprite InGameIcon => weaponInGameIcon;

        public float Damage => _damage;
        
        [OdinSerialize][PreviewField(100, ObjectFieldAlignment.Center)][HideLabel] private Sprite weaponInGameIcon;

        [SerializeField] private float _damage;
    }
}