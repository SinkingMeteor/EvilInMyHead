using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticConfigData : IDatabaseItem
    {
        public string ID => TypeName;

        public string TypeName;
        public string GroupName;
        public int Cost;
        public int MaxStack;
        public string GameIcon;
        public bool IsEquippable;
        public bool IsStackable;
        public bool IsQuest;
        public bool IsUsable;
        
    }
}