
namespace Sheldier.Actors
{
    public interface IActorModule
    {
        int Priority { get; }
        public void Initialize(IActorModuleCenter actorModuleCenter);
        public void Tick();
    }
}
