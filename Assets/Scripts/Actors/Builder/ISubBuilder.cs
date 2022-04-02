using Sheldier.Actors.Data;

namespace Sheldier.Actors.Builder
{
    public interface ISubBuilder
    {
        void Build(Actor actor, ActorStaticBuildData buildData);
    }
}