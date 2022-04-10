using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Data
{
    public class StatsConfigImporter : SheetLoader
    {
        [SerializeField] private bool _prettyPrint;
        
        [Button]
        public void NumericalStatsDataToJson()
        {
            LoadAll(new string[] {"ActorNumericalStatData"}, ProcessNumericalStatData);
        }

        private void ProcessNumericalStatData(string[] datas)
        {
            List<StaticNumericalStatCollection> numericalConfig = new List<StaticNumericalStatCollection>();

            foreach (var data in datas)
            {
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0);

                var firstLine = lines[0].Split(new[] {","}, StringSplitOptions.None).ToList();
                string typeName = firstLine[0];
                List<StaticNumericalStatData> statDatas = new List<StaticNumericalStatData>();
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    if (typeName != items[0])
                    {
                        var collection = new StaticNumericalStatCollection(statDatas.ToArray());
                        collection.TypeName = typeName;
                        numericalConfig.Add(collection);
                        statDatas.Clear();
                        typeName = items[0];
                    }

                    var model = new StaticNumericalStatData()
                    {
                        TypeName = items[0],
                        StatName = items[1],
                        BaseValue = float.Parse(items[2], CultureInfo.InvariantCulture)
                    };
                    statDatas.Add(model);
                }
                var staticCollection = new StaticNumericalStatCollection(statDatas.ToArray());
                staticCollection.TypeName = typeName;
                numericalConfig.Add(staticCollection);
            }
            Save(numericalConfig.ToArray(), "NumericalStatsData",_prettyPrint);
        }

        [Button]
        public void StringStatsDataToJson()
        {
            LoadAll(new string[] {"ActorStringStatData"}, ProcessStringStatData);
        }

        private void ProcessStringStatData(string[] datas)
        {
            List<StaticStringStatCollection> stringConfig = new List<StaticStringStatCollection>();

            foreach (var data in datas)
            {
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0);

                var firstLine = lines[0].Split(new[] {","}, StringSplitOptions.None).ToList();
                string typeName = firstLine[0];
                List<StaticStringStatData> statDatas = new List<StaticStringStatData>();
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    if (typeName != items[0])
                    {
                        var collection = new StaticStringStatCollection(statDatas.ToArray());
                        collection.TypeName = typeName;
                        stringConfig.Add(collection);
                        statDatas.Clear();
                        typeName = items[0];
                    }

                    var model = new StaticStringStatData()
                    {
                        TypeName = items[0],
                        StatName = items[1],
                        Value = items[2]
                    };
                    statDatas.Add(model);
                }
                var staticCollection = new StaticStringStatCollection(statDatas.ToArray());
                staticCollection.TypeName = typeName;
                stringConfig.Add(staticCollection);
            }
            Save(stringConfig.ToArray(), "StringStatsData",_prettyPrint);
        }
    }
}