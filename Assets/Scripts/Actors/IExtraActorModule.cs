using Sheldier.Factories;

namespace Sheldier.Actors
{
    public interface IExtraActorModule
    {
        int Priority { get; }
        void Initialize(ActorInternalData data);
        void Dispose();
    }
}
