﻿using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(menuName = "Sheldier/Items/Map", fileName = "ItemMap")]
    public class ItemMap : BaseScriptableObject
    {
        public IReadOnlyDictionary<ItemType, ItemConfig> ItemsMap => _itemsCollection;
        public IReadOnlyDictionary<ItemType, WeaponConfig> WeaponMap => _weaponMap;

        [OdinSerialize] private Dictionary<ItemType, WeaponConfig> _weaponMap;
        
        private Dictionary<ItemType, ItemConfig> _itemsCollection;

        public void OnValidate()
        {
            _itemsCollection = new Dictionary<ItemType, ItemConfig>();

            foreach (var weaponConfig in _weaponMap)
                _itemsCollection.Add(weaponConfig.Key, weaponConfig.Value);
        }
    }
}