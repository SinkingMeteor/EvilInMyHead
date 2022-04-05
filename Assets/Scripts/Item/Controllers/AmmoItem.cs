using System;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using UnityEngine;

namespace Sheldier.Item
{
    public class AmmoItem : SimpleItem
    {
        public override void Initialize()
        {
            
        }

        public override void Drop()
        {
            Debug.Log("Ammo dropped");
        }

        public override void Equip(IHandView handView, Actor owner)
        {
            throw new ArgumentException("This item is unequippable");
        }

        public override void Unequip()
        {
            throw new ArgumentException("This item is unequippable");
        }
        
        public AmmoItem CleanClone(string guid) => new AmmoItem(guid);

        public AmmoItem(string guid) : base(guid)
        {
        }
    }
}