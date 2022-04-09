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
            List<StaticNumericalStatData> numericalConfig = new List<StaticNumericalStatData>();

            foreach (var data in datas)
            {
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0);
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new StaticNumericalStatData()
                    {
                        TypeName = items[0],
                        StatName = items[1],
                        BaseValue = float.Parse(items[2], CultureInfo.InvariantCulture)
                    };
                    numericalConfig.Add(model);
                }
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
            List<StaticStringStatData> stringConfig = new List<StaticStringStatData>();

            foreach (var data in datas)
            {
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0);
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new StaticStringStatData()
                    {
                        TypeName = items[0],
                        StatName = items[1],
                        Value = items[2]
                    };
                    stringConfig.Add(model);
                }
            }
            Save(stringConfig.ToArray(), "StringStatsData",_prettyPrint);
        }
    }
}