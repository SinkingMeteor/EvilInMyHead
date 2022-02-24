using System;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using UnityEngine;

namespace Sheldier.Item
{
    public class AmmoItem : SimpleItem
    {
        public override bool IsEquippable => false;
        public override bool IsStackable => true;

        public AmmoItem(ItemConfig itemConfig) : base(itemConfig)
        {
        }

        public override void Drop()
        {
            Debug.Log("Ammo dropped");
        }

        public override void Equip(HandView handView, Actor owner)
        {
            throw new ArgumentException("This item is unequippable");
        }

        public override void Unequip()
        {
            throw new ArgumentException("This item is unequippable");
        }
    }
}