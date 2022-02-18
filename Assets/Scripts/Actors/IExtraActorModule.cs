
namespace Sheldier.Actors
{
    public interface IExtraActorModule
    {
        int Priority { get; }
        public void Initialize(IActorModuleCenter moduleCenter);
        public void Tick();
    }
}
