using System;
using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Actors.Inventory;
using Sheldier.Common.Utilities;
using UnityEngine;

namespace Sheldier.Item
{
    public abstract class SimpleItem
    {
        public ItemDynamicConfigData ItemConfig => _itemConfig;

        protected ItemDynamicConfigData _itemConfig;
        public void SetDynamicConfig(ItemDynamicConfigData dynamicConfigData)
        {
            _itemConfig = dynamicConfigData;
        }
        
        public virtual Vector2 GetRotateDirection()
        {
            return Vector2.zero;
        }
        public abstract void Drop();

        public abstract void Equip(HandView handView, Actor owner);

        public abstract void Unequip();

        public virtual string GetExtraInfo()
        {
            return String.Empty;
        }
    }
}