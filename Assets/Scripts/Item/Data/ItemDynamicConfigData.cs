using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemDynamicConfigData : IDatabaseItem
    {
        public string ID => _guid;
        public string TypeName => _typeName;
        public string GroupName => _groupName;
        public string GameIcon => _gameIcon;
        public bool IsEquippable => _isEquippable;
        public bool IsStackable => _isStackable;
        public bool IsQuest => _isQuest;
        public bool IsUsable => _isUsable;

        public int Amount;
        
        public int Cost;
        
        public int MaxStack;

        
        private string _guid;
        private string _typeName;
        private string _groupName;
        private string _gameIcon;
        private bool _isEquippable;
        private bool _isStackable;
        private bool _isQuest;
        private bool _isUsable;

        public ItemDynamicConfigData(string guid, ItemStaticConfigData staticConfigData)
        {
            _guid = guid;
            Cost = staticConfigData.Cost;
            MaxStack = staticConfigData.MaxStack;
            _typeName = staticConfigData.TypeName;
            _groupName = staticConfigData.GroupName;
            _gameIcon = staticConfigData.GameIcon;
            _isEquippable = staticConfigData.IsEquippable;
            _isStackable = staticConfigData.IsStackable;
            _isQuest = staticConfigData.IsQuest;
            _isUsable = staticConfigData.IsUsable;
            Amount = UnityEngine.Random.Range(1, MaxStack + 1);
        }
    }
}