using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.GameLocation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Data
{
    public class LocationConfigImporter : SheetLoader
    {
        [SerializeField] private bool _prettyPrint = false;

        [Button]
        private void LocationDataToJson()
        {
            Load("LocationData", data =>
            {
                var config = new List<LocationStaticConfig>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new LocationStaticConfig()
                    {
                        TypeName = items[0],
                        VolumeProfile = items[1],
                        OnLoadCutscene = items[2] == "None" ? null : items[2]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "LocationData",_prettyPrint);
            });
        }
    }
}