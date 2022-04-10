using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Data
{
    public class DialoguesConfigImporter : SheetLoader
    {
        [SerializeField] private bool _prettyPrint = false;

        [Button]
        private void DialoguesDataToJson()
        {
            Load("DialoguesData", data =>
            {
                var config = new List<DialogueStaticData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new DialogueStaticData()
                    {
                        OwnerNameID = items[0],
                        DialogueName = items[1],
                        NextDialogueName = items[2]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "DialoguesData",_prettyPrint);
            });
        }
    }
}