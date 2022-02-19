using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Item
{
    public abstract class WeaponConfig : ItemConfig
    {
        public float Damage => _damage;
        public override ItemGroup ItemGroup => ItemGroup.Weapon;

        [SerializeField] private float _damage;
    }
}