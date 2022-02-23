using System;
using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using UnityEngine;

namespace Sheldier.Item
{
    public class AmmoItem : SimpleItem
    {
        public override bool IsEquippable => false;
        private Actor _owner;

        public AmmoItem(ItemConfig itemConfig) : base(itemConfig)
        {
        }

        public override void PutToInventory(Actor owner, Dictionary<ItemConfig, List<SimpleItem>> itemsCollection)
        {
            _owner = owner;
            if(itemsCollection[_itemConfig].Count > 0)
                itemsCollection[_itemConfig][0].ItemAmount.Add(2);
            else
                itemsCollection[_itemConfig].Add(this);                
        }

        public override int RemoveItem(Dictionary<ItemConfig, List<SimpleItem>> itemsCollection, int amountToRemove)
        {
            if (amountToRemove < _itemAmount.Amount)
            {
                _itemAmount.Remove(amountToRemove);
                return amountToRemove;
            }
            itemsCollection.Remove(_itemConfig);
            return _itemAmount.Amount;
        }

        protected override void Drop()
        {
            Debug.Log("Ammo dropped");
        }

        public override void Equip(HandView handView)
        {
            throw new ArgumentException("This item is unequippable");
        }

        public override void Unequip()
        {
            throw new ArgumentException("This item is unequippable");
        }
    }
}