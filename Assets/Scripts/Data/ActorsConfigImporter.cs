using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sheldier.Actors.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Data
{
    public class ActorsConfigImporter : SheetLoader
    {
        [SerializeField] private bool _prettyPrint = false;

        [Button]
        public void ActorStaticConfigToJson()
        {
            Load("ActorConfig", data =>
            {
                var config = new List<ActorStaticConfigData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ActorStaticConfigData()
                    {
                        TypeName = items[0],
                        ActorAppearance = items[1],
                        ActorAvatar = items[2],
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ActorConfig",_prettyPrint);
            });
        }
        [Button]
        public void ActorStaticBuildDataToJson()
        {
            Load("ActorBuildData", data =>
            {
                var config = new List<ActorStaticBuildData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ActorStaticBuildData()
                    {
                        TypeName = items[0],
                        CanMove = Convert.ToBoolean(items[1]),
                        CanEquip = Convert.ToBoolean(items[2]),
                        IsEffectPerceptive = Convert.ToBoolean(items[3]),
                        CanInteract = Convert.ToBoolean(items[4]),
                        CanAttack = Convert.ToBoolean(items[5]),
                        CanJump = Convert.ToBoolean(items[6]),
                        InteractType = items[7],
                        CanMoveObjects = Convert.ToBoolean(items[8])
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ActorBuildData",_prettyPrint);
            });
        }
    }
}