using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticProjectileData : IDatabaseItem
    {
        public string ID => TypeID;
        
        public string ItemName;
        public string TypeID;
        public float Duration;
        public float Speed;
        public string Icon;
    }
}