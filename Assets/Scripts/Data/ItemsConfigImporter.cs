using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sheldier.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Data
{
    public class ItemsConfigImporter : SheetLoader
    {
        [SerializeField] private bool _prettyPrint = false;

        [Button]
        private void ActorStaticConfigToJson()
        {
            Load("ItemConfig", data =>
            {
                var config = new List<ItemStaticConfigData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ItemStaticConfigData()
                    {
                        TypeName = items[0],
                        GroupName = items[1],
                        Cost = int.Parse(items[2]),
                        MaxStack = int.Parse(items[3]),
                        GameIcon = items[4],
                        IsEquippable = bool.Parse(items[5]),
                        IsStackable = bool.Parse(items[6]),
                        IsQuest = bool.Parse(items[7]),
                        IsUsable = bool.Parse(items[8]),
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ItemConfig",_prettyPrint);
            });
        }
        [Button]
        private void ActorStaticWeaponDataToJson()
        {
            Load("WeaponConfig", data =>
            {
                var config = new List<ItemStaticWeaponData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ItemStaticWeaponData()
                    {
                        TypeName = items[0],
                        GroupName = items[1],
                        ProjectileName = items[2],
                        Damage = float.Parse(items[3], CultureInfo.InvariantCulture),
                        FireRate = float.Parse(items[4], CultureInfo.InvariantCulture),
                        Capacity = int.Parse(items[5]),
                        AimLocalX = float.Parse(items[6], CultureInfo.InvariantCulture),
                        AimLocalY = float.Parse(items[7], CultureInfo.InvariantCulture),
                        BlowAnimation = items[8],
                        UseAudio = items[9],
                        RequiredAmmoItemName = items[10],
                        ReloadAnimation = items[11],
                        ReloadAudio = items[12]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "WeaponConfig",_prettyPrint);
            });
        }
        [Button]
        private void ActorStaticProjectileDataToJson()
        {
            Load("ProjectileConfig", data =>
            {
                var config = new List<ItemStaticProjectileData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ItemStaticProjectileData()
                    {
                        TypeName = items[0],
                        Duration = float.Parse(items[1], CultureInfo.InvariantCulture), 
                        Speed = float.Parse(items[2], CultureInfo.InvariantCulture),
                        Icon = items[3]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "ProjectileConfig",_prettyPrint);
            });
        }
        [Button]
        private void ActorStaticInventorySlotDataToJson()
        {
            Load("InventorySlot", data =>
            {
                var config = new List<ItemStaticInventorySlotData>();
                var lines = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
                lines.RemoveAt(0); // headers
                foreach (var line in lines)
                {
                    var items = line.Split(new[] {","}, StringSplitOptions.None).ToList();
                    var model = new ItemStaticInventorySlotData()
                    {
                       TypeName = items[0],
                       ItemTitle = items[1],
                       ItemDescription = items[2],
                       Icon = items[3]
                    };
                    config.Add(model);
                }
                Save(config.ToArray(), "InventorySlot",_prettyPrint);
            });
        }
    }
}