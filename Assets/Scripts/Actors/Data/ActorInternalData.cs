using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorInternalData
    {
        public ActorTransformHandler ActorTransformHandler => _transformHandler;
        public TickHandler TickHandler => _tickHandler;
        public Actor Actor => _actor;
        public Rigidbody2D Rigidbody2D => _actorsRigidbody;
        public ActorTraceProvider ActorTraceProvider => _actorTraceProvider;

        private readonly ActorTransformHandler _transformHandler;
        private readonly TickHandler _tickHandler;
        private readonly Actor _actor;
        private readonly Rigidbody2D _actorsRigidbody;
        private readonly ActorTraceProvider _actorTraceProvider;


        public ActorInternalData(ActorTransformHandler transformHandler, TickHandler tickHandler, Actor actor,
            Rigidbody2D actorsRigidbody, ActorTraceProvider actorTraceProvider)
        {
            _tickHandler = tickHandler;
            _actor = actor;
            _actorsRigidbody = actorsRigidbody;
            _actorTraceProvider = actorTraceProvider;
            _transformHandler = transformHandler;
        }
    }
}