using Sheldier.Actors;
using Sheldier.Actors.Hand;

namespace Sheldier.Item
{
    public class NullItem : SimpleItem
    {
        public NullItem(string id = null) : base(id)
        {
        }
        
        public override void Initialize()
        {
            
        }

        public override void Drop()
        {
            
        }

        public override void Equip(IHandView handView, Actor owner)
        {
        }
        
        public override void Unequip()
        {
        }

       
    }
}