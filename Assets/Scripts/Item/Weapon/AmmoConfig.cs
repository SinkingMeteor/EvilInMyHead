using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(menuName = "Sheldier/Items/AmmoConfig", fileName = "AmmoConfig")]
    public class AmmoConfig : ItemConfig
    {
        public override ItemGroup ItemGroup => ItemGroup.Ammo;
    }
}