using Sheldier.Data;
using Sheldier.UI;
using UnityEngine;

namespace Sheldier.Common.Pool
{
    public class InventorySlotPool : DefaultPool<InventorySlot>
    {
        private AssetProvider<Sprite> _spriteProvider;
        public void SetDependencies(AssetProvider<Sprite> spriteProvider)
        {
            _spriteProvider = spriteProvider;
        }
        protected override void SetDependenciesToEntity(InventorySlot entity)
        {
            entity.SetDependencies(_spriteProvider);
        }
    }
}