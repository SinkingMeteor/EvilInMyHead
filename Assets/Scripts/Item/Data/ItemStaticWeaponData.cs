using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemStaticWeaponData : IDatabaseItem
    {
        public string ID => TypeName;
        
        public string TypeName;
        public string GroupName;
        public string ProjectileName;
        public float Damage;
        public float FireRate;
        public int Capacity;
        public float AimLocalX;
        public float AimLocalY;
        public string BlowAnimation;
        public string UseAudio;
        public string RequiredAmmoItemName;
        public string ReloadAnimation;
        public string ReloadAudio;
    }
}