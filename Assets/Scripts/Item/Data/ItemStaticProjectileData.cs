using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticProjectileData : IDatabaseItem
    {
        public string ID => TypeName;
        
        public string TypeName;
        public float Duration;
        public float Speed;
        public string Icon;
    }
}