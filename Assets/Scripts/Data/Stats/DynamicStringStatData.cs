using System;

namespace Sheldier.Data
{
    [Serializable]
    public class DynamicStringStatData : IDatabaseItem
    {
        public string ID => _statName;

        public string Value;
        public string StatName => _statName;
        public string TypeName => _typeName;

        private string _typeName;
        private string _statName;
        public DynamicStringStatData(StaticStringStatData staticData)
        {
            _typeName = staticData.TypeName;
            _statName = staticData.StatName;
            Value = staticData.Value;
        }
    }
    
    
}