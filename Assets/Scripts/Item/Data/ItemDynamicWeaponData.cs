using System;
using Sheldier.Data;

namespace Sheldier.Item
{
    [Serializable]
    public class ItemDynamicWeaponData : IDatabaseItem
    {
        public string ID => Guid;
        public float AimLocalX => _aimLocalX;
        public float AimLocalY => _aimLocalY;
        public string ProjectileName => _projectileName;
        public string TypeName => _typeName;
        public string GroupName => _groupName;
        public string BlowAnimation => _blowAnimation;
        public string UseAudio => _useAudio;
        public string RequiredAmmoItemName => _requiredAmmoItemName;
        public string ReloadAnimation => _reloadAnimation;
        public string ReloadAudio => _reloadAudio;

        public string Guid;
        public float Damage;
        public float FireRate;
        public int Capacity;
        public int AmmoLeft;

        private float _aimLocalX;
        private float _aimLocalY;
        private string _projectileName;
        private string _typeName;
        private string _groupName;
        private string _blowAnimation;
        private string _useAudio;
        private string _requiredAmmoItemName;
        private string _reloadAnimation;
        private string _reloadAudio;

        public ItemDynamicWeaponData(string guid, ItemStaticWeaponData staticWeaponData)
        {
            Guid = guid;
            _typeName = staticWeaponData.TypeName;
            _groupName = staticWeaponData.GroupName;
            _projectileName = staticWeaponData.ProjectileName;
            Damage = staticWeaponData.Damage;
            FireRate = staticWeaponData.FireRate;
            Capacity = staticWeaponData.Capacity;
            _aimLocalX = staticWeaponData.AimLocalX;
            _aimLocalY = staticWeaponData.AimLocalY;
            _blowAnimation = staticWeaponData.BlowAnimation;
            _useAudio = staticWeaponData.UseAudio;
            _requiredAmmoItemName = staticWeaponData.RequiredAmmoItemName;
            _reloadAnimation = staticWeaponData.ReloadAnimation;
            _reloadAudio = staticWeaponData.ReloadAudio;
            AmmoLeft = UnityEngine.Random.Range(0, Capacity);
        }
    }
}