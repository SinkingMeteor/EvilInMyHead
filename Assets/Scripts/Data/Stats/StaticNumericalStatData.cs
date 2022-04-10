using System;

namespace Sheldier.Data
{

    [Serializable]
    public struct StaticNumericalStatData
    {
        public string ID => StatName;
        
        public string TypeName;
        public string StatName;
        public float BaseValue;
    }
}

