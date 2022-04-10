namespace Sheldier.Actors.Builder
{
    public interface IActorBuilder
    {
        public Actor Build(string typeID, string guid, ActorPlaceholder actorPlaceholder);
    }
}