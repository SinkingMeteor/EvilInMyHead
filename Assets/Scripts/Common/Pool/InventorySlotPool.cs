using Sheldier.Data;
using Sheldier.Item;
using Sheldier.UI;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class InventorySlotPool : DefaultPool<InventorySlot>
    {
        private Database<ItemDynamicConfigData> _dynamicConfigDatabase;
        private AssetProvider<Sprite> _spriteProvider;

        [Inject]
        public void InjectDependencies(AssetProvider<Sprite> spriteProvider, Database<ItemDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _spriteProvider = spriteProvider;
        }
        protected override void SetDependenciesToEntity(InventorySlot entity)
        {
            entity.SetDependencies(_spriteProvider, _dynamicConfigDatabase);
        }
    }
}