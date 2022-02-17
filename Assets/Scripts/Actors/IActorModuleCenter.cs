namespace Sheldier.Actors
{
    public interface IActorModuleCenter
    {
        ActorInputController ActorInputController { get; }
        ActorTransformHandler ActorTransformHandler { get; }
    }
}