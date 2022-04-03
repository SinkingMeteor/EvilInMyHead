using Sheldier.Actors;
using Sheldier.Actors.Hand;

namespace Sheldier.Item
{
    public class NullItem : SimpleItem
    {
        public NullItem(ItemConfig itemConfig = null) : base(itemConfig)
        {
        }

        public override void Drop()
        {
            
        }

        public override void Equip(HandView handView, Actor owner)
        {
        }

        public override void Unequip()
        {
        }
    }
}