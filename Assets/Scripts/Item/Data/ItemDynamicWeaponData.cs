using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemDynamicWeaponData : IDatabaseItem
    {
        public string ID => Guid;

        public string Guid;
        public string ItemName;
        public int TypeID;
        public string GroupName;
        public int GroupID;
        public string ProjectileName;
        public string ProjectileID;
        public float Damage;
        public float FireRate;
        public int Capacity;
        public float AimLocalX;
        public float AimLocalY;
        public string BlowAnimation;
        public string ReloadAnimation;
        public string UseAudio;
        public string ReloadAudio;
        public string RequiredAmmoItemName;
        public int AmmoLeft;

        public ItemDynamicWeaponData(string guid, ItemStaticWeaponData staticWeaponData)
        {
            ItemName = staticWeaponData.ItemName;
            TypeID = staticWeaponData.TypeID;
            GroupName = staticWeaponData.GroupName;
            GroupID = staticWeaponData.GroupID;
            ProjectileName = staticWeaponData.ProjectileName;
            ProjectileID = staticWeaponData.ProjectileID;
            Damage = staticWeaponData.Damage;
            FireRate = staticWeaponData.FireRate;
            Capacity = staticWeaponData.Capacity;
            AimLocalX = staticWeaponData.AimLocalX;
            AimLocalY = staticWeaponData.AimLocalY;
            BlowAnimation = staticWeaponData.BlowAnimation;
            UseAudio = staticWeaponData.UseAudio;
            ReloadAudio = staticWeaponData.ReloadAudio;
        }
    }
}