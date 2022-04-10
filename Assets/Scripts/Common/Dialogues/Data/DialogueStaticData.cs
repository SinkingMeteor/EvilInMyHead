using System;
using Sheldier.Data;

namespace Sheldier.Common
{
    [Serializable]
    public struct DialogueStaticData : IDatabaseItem
    {
        public string ID => DialogueName;
        
        public string OwnerNameID;
        public string DialogueName;
        public string NextDialogueName;
    }
}