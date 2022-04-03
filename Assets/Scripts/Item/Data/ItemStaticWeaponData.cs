using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticWeaponData : IDatabaseItem
    {
        public string ID => ItemName;
        
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
        public string UseAudio;
        public string ReloadAudio;
    }
}