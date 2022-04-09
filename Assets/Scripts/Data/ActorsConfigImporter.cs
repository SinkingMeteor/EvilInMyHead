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
                        ActorAvatar = items[2]
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
                        InteractType = items[7]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ActorBuildData",_prettyPrint);
            });
        }
        [Button]
        public void ActorStaticMovementDataToJson()
        {
            Load("ActorMovementData", data =>
            {
                var config = new List<ActorStaticMovementData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ActorStaticMovementData()
                    {
                        TypeName = items[0],
                        Speed = float.Parse(items[1]),
                        StepSound = items[2]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ActorMovementData",_prettyPrint);
            });
        }
        [Button]
        public void ActorStaticDialogueDataToJson()
        {
            Load("ActorDialogueData", data =>
            {
                var config = new List<ActorStaticDialogueData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0);
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ActorStaticDialogueData()
                    {
                        TypeName = items[0],
                        TypeSpeed = float.Parse(items[1], CultureInfo.InvariantCulture),
                        TextColorR = float.Parse(items[2],CultureInfo.InvariantCulture),
                        TextColorG = float.Parse(items[3],CultureInfo.InvariantCulture),
                        TextColorB = float.Parse(items[4],CultureInfo.InvariantCulture),
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ActorDialogueData",_prettyPrint);
            });
        }
    }
}