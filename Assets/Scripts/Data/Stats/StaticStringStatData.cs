using System;

namespace Sheldier.Data
{
    [Serializable]
    public struct StaticStringStatData
    {
        public string ID => TypeName + StatName;

        public string TypeName;
        public string StatName;
        public string Value;
    }
}