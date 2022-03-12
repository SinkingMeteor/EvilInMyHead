
namespace Sheldier.Common
{
    public class FixedTickHandler : BaseTickHandler<IFixedTickListener>
    {
        protected override void OnTick(IFixedTickListener tickListener) => tickListener.FixedTick();
    }
}