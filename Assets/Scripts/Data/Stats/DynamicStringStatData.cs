using System;

namespace Sheldier.Data
{
    [Serializable]
    public class DynamicStringStatData : IDatabaseItem
    {
        public string ID => _statName;

        public string Value => _value;
        public string StatName => _statName;
        public string TypeName => _typeName;

        private string _typeName;
        private string _statName;
        private string _value;
        public DynamicStringStatData(StaticStringStatData staticData)
        {
            _typeName = staticData.TypeName;
            _statName = staticData.StatName;
            _value = staticData.Value;
        }
    }
    
    
}