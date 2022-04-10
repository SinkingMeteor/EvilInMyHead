using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheldier.Data
{
    [Serializable]
    public class StaticNumericalStatCollection : IDatabaseItem
    {
        public string ID => TypeName;
        public string TypeName;
        public Dictionary<string, StaticNumericalStatData> StatsCollection;

        public StaticNumericalStatCollection(StaticNumericalStatData[] datas)
        {
            StatsCollection = datas.ToDictionary(x => x.ID);
        }
        public StaticNumericalStatCollection(){}
    }
}