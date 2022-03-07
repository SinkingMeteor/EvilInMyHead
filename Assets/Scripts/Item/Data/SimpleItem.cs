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
        public ItemConfig ItemConfig => _itemConfig;
        public virtual bool IsEquippable => true;
        public virtual bool IsStackable => false;
        public Counter ItemAmount => _itemAmount;
        public int FreeAmount => _itemConfig.MaxStack - _itemAmount.Amount; 

        protected Counter _itemAmount;

        protected readonly ItemConfig _itemConfig;

        public SimpleItem(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
            _itemAmount = new Counter(1);
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