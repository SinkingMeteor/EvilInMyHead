using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemDynamicConfigData : IDatabaseItem
    {
        public string ID => Guid;

        public string Guid;
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
        public int Amount;

        public ItemDynamicConfigData(string guid, ItemStaticConfigData staticConfigData)
        {
            Guid = guid;
            ItemName = staticConfigData.ItemName;
            TypeID = staticConfigData.TypeID;
            GroupName = staticConfigData.GroupName;
            GroupID = staticConfigData.GroupID;
            Cost = staticConfigData.Cost;
            MaxStack = staticConfigData.MaxStack;
            GameIcon = staticConfigData.GameIcon;
            IsEquippable = staticConfigData.IsEquippable;
            IsStackable = staticConfigData.IsStackable;
            IsQuest = staticConfigData.IsQuest;
            Amount = 1;
        }
    }
}