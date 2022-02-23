using Sheldier.Factories;

namespace Sheldier.Actors
{
    public interface IActorModuleCenter
    {
        ActorInputController ActorInputController { get; }
        ActorTransformHandler ActorTransformHandler { get; }
        IActorEffectModule ActorEffectModule { get; }
        ActorNotifyModule Notifier{ get; }
        ItemFactory ItemFactory { get; }
        bool TryGetModule<T>(out T module) where T : class;
    }
}