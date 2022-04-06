using System;
using Sheldier.Actors;
using Sheldier.Actors.Hand;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Item
{
    public abstract class SimpleItem : IDatabaseItem
    {
        public string ID => _guid;

        protected string _guid;

        protected SimpleItem(string id = null)
        {
            _guid = id;
        }
        
        public virtual Vector2 GetRotateDirection()
        {
            return Vector2.zero;
        }

        public abstract void Initialize();
        public abstract void Drop();

        public abstract void Equip(IHandView handView, Actor owner);

        public abstract void Unequip();

        public virtual string GetExtraInfo()
        {
            return String.Empty;
        }
    }
}