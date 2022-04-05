using Sheldier.Data;

namespace Sheldier.Item
{
    public class ItemStaticWeaponDatabase : Database<ItemStaticWeaponData>
    { }

    public class ItemDataProvider
    {
        
    }

    public interface IItemConfigProvider
    {
        ItemStaticConfigData GetStaticConfigByGuid(string guid);
        ItemStaticConfigData GetStaticConfigByTypeName(string typeName);
        ItemDynamicConfigData GetDynamicConfig(string guid);
    }

    public interface IItemWeaponDataProvider
    {
        ItemStaticWeaponData GetStaticWeaponDataByGuid(string guid);
        ItemStaticWeaponData GetStaticWeaponDataByTypeName(string typeName);
        ItemDynamicWeaponData GetDynamicWeaponData(string guid);
    }

    public interface IItemProjectileDataProvider
    {
        ItemStaticProjectileData GetStaticProjectileDataByGuid(string guid);
        ItemStaticProjectileData GetStaticProjectileDataByTypeName(string typeName);
        ItemStaticProjectileData GetDynamicProjectileData(string guid);
    }
}