using System;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using UnityEngine;

namespace Sheldier.Item
{
    public class AmmoItem : SimpleItem
    {
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

        public override string GetExtraInfo()
        {
            return _itemConfig.Amount.ToString();
        }

        public AmmoItem CleanClone() => new AmmoItem();
    }
}