using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticInventorySlotData : IDatabaseItem
    {
        public string ID => TypeName;
        
        public string TypeName;
        public string ItemTitle;
        public string ItemDescription;
        public string Icon;


    }
}