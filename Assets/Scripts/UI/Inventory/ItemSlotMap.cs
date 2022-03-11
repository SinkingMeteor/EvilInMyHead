using System.Collections.Generic;
using Sheldier.Item;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.UI
{
    [CreateAssetMenu(menuName = "Sheldier/Items/ItemSlotMap", fileName = "ItemSlotMap")]
    public class ItemSlotMap : BaseScriptableObject
    {
        public ItemSlotData CancelSlot => cancelSlot;
        public IReadOnlyDictionary<ItemConfig, ItemSlotData> SlotMap => slotMap;
        public IReadOnlyDictionary<InventoryHintPerformType, string> HintTitleMap => hintTitleMap;
        
        [OdinSerialize] private Dictionary<ItemConfig, ItemSlotData> slotMap;
        [OdinSerialize] private ItemSlotData cancelSlot;
        [OdinSerialize] private Dictionary<InventoryHintPerformType, string> hintTitleMap;
    }
}