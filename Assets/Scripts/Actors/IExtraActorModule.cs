using Sheldier.Factories;

namespace Sheldier.Actors
{
    public interface IExtraActorModule
    {
        void Initialize(ActorInternalData data);
        void Dispose();
    }
}
