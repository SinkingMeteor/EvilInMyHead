using Sheldier.UI;

namespace Sheldier.Common.Pool
{
    public class InventorySlotPool : DefaultPool<InventorySlot>
    {
        protected override void SetDependenciesToEntity(InventorySlot entity)
        {
        }
    }
}