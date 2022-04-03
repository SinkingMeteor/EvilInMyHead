using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticConfigData : IDatabaseItem
    {
        public string ID => ItemName;

        public string ItemName;
        public int TypeID;
        public string GroupName;
        public int GroupID;
        public int Cost;
        public int MaxStack;
        public string GameIcon;
        public bool IsEquippable;
        public bool IsStackable;
        public bool IsQuest;
        
    }
}