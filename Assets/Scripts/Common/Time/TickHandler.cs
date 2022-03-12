
namespace Sheldier.Common
{
    public class TickHandler : BaseTickHandler<ITickListener>
    {
        protected override void OnTick(ITickListener tickListener) => tickListener.Tick();
    }
}
