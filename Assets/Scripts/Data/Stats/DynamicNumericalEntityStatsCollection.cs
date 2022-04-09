using System;

namespace Sheldier.Data
{
    [Serializable]
    public class DynamicNumericalEntityStatsCollection : Database<DynamicNumericalStatData>, IDatabaseItem
    {
        public string ID => _guid;
        private string _guid;

        public DynamicNumericalEntityStatsCollection(string guid)
        {
            _guid = guid;
        }
    }

    [Serializable]
    public class DynamicStringEntityStatsCollection : Database<DynamicStringStatData>, IDatabaseItem
    {
        public string ID => _guid;
        private string _guid;
        
        public DynamicStringEntityStatsCollection(string guid)
        {
            _guid = guid;
        }
    }
    
}