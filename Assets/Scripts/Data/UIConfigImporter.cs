using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Data
{
    public class UIConfigImporter : SheetLoader
    {
        [SerializeField] private bool _prettyPrint = false;

        [Button]
        private void UIPerformDataToJson()
        {
            Load("UIPerformData", data =>
            {
                var config = new List<UIPerformStaticData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new UIPerformStaticData()
                    {
                        PerformType = items[0],
                        Localization = items[1]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "UIPerformData",_prettyPrint);
            });
        }
    
    }
}