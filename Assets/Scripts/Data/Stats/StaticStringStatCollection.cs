using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheldier.Data
{
    [Serializable]
    public class StaticStringStatCollection : IDatabaseItem
    {
        public string ID => TypeName;
        public string TypeName;
        public Dictionary<string, StaticStringStatData> StatsCollection;

        public StaticStringStatCollection(StaticStringStatData[] datas)
        {
            StatsCollection = datas.ToDictionary(x => x.ID);
        }
        public StaticStringStatCollection(){}
    }
}