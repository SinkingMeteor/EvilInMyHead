
namespace Sheldier.Common
{
    public class LateTickHandler : BaseTickHandler<ILateTickListener>
    {
        protected override void OnTick(ILateTickListener tickListener) => tickListener.LateTick();
    }
}