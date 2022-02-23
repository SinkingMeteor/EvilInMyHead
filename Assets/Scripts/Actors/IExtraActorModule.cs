using Sheldier.Factories;

namespace Sheldier.Actors
{
    public interface IExtraActorModule
    {
        int Priority { get; }
        public void Initialize(ActorInternalData data);
        void Dispose();
    }
}
