using Sheldier.Common;
using Sheldier.Factories;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorInternalData
    {
        public ActorInputController ActorInputController => _actorInputController;
        public ActorTransformHandler ActorTransformHandler => _transformHandler;
        public IActorEffectModule ActorEffectModule => _actorEffectModule;
        public ActorNotifyModule Notifier => _notifier; 
        public TickHandler TickHandler => _tickHandler;
        public Actor Actor => _actor;
        public SpriteRenderer Sprite => _spriteRenderer;

        private readonly ActorNotifyModule _notifier;
        private readonly ActorTransformHandler _transformHandler;
        private readonly ActorInputController _actorInputController;
        private readonly ActorEffectModule _actorEffectModule;
        private readonly TickHandler _tickHandler;
        private readonly Actor _actor;
        private readonly SpriteRenderer _spriteRenderer;

        public ActorInternalData(ActorInputController inputController, ActorTransformHandler transformHandler,
            ActorEffectModule effectModule,
            ActorNotifyModule notifier, TickHandler tickHandler, Actor actor, SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _actor = actor;
            _tickHandler = tickHandler;
            _actorInputController = inputController;
            _transformHandler = transformHandler;
            _notifier = notifier;
            _actorEffectModule = effectModule;
        }

    }
}