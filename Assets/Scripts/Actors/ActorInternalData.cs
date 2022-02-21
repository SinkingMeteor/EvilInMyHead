using Sheldier.Common;
using Sheldier.Factories;

namespace Sheldier.Actors
{
    public class ActorInternalData
    {
        public ActorInputController ActorInputController => _actorInputController;
        public ActorTransformHandler ActorTransformHandler => _transformHandler;
        public IActorEffectModule ActorEffectModule => _actorEffectModule;
        public ActorNotifyModule Notifier => _notifier; 
        public ItemFactory ItemFactory => _itemFactory;
        public TickHandler TickHandler => _tickHandler;

        private ActorNotifyModule _notifier;
        private ActorTransformHandler _transformHandler;
        private ActorInputController _actorInputController;
        private ItemFactory _itemFactory;
        private ActorEffectModule _actorEffectModule;
        private TickHandler _tickHandler;

        public ActorInternalData(ActorInputController inputController, ActorTransformHandler transformHandler,
            ActorEffectModule effectModule,
            ActorNotifyModule notifier, ItemFactory itemFactory, TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
            _actorInputController = inputController;
            _transformHandler = transformHandler;
            _notifier = notifier;
            _actorEffectModule = effectModule;
            _itemFactory = itemFactory;
        }

    }
}