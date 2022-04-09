using System;

namespace Sheldier.Data
{

    [Serializable]
    public struct StaticNumericalStatData : IDatabaseItem
    {
        public string ID => TypeName + StatName;
        
        public string TypeName;
        public string StatName;
        public float BaseValue;
    }
}

