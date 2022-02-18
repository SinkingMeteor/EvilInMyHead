namespace Sheldier.Actors
{
    public interface IActorModuleCenter
    {
        ActorInputController ActorInputController { get; }
        ActorTransformHandler ActorTransformHandler { get; }
        IActorEffectModule ActorEffectModule { get; }
        IGrabNotifier GrabNotifier { get; }
        bool TryGetModule<T>(out T module) where T : class;
    }
}