using Sheldier.Common.Utilities;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorTraceProvider
    {
        public Vector3 LastSafePoint => _lastSafePoint;
        
        private Vector3 _lastSafePoint;
        private Transform _actorTransform;
        private float _accumulatedTime;

        private const float DELAY = 1.0f;
        public void Initialize()
        {
            _lastSafePoint = _actorTransform.position;
        }
        public void SetDependencies(Transform actor)
        {
            _actorTransform = actor;
        }

        public void Tick()
        {
            _accumulatedTime += Time.deltaTime;
            if (_accumulatedTime > DELAY)
            {
                if (!Physics2D.OverlapCircle(_actorTransform.position.DiscardZ(), 0.1f,
                        EnvironmentConstants.PIT_LAYER_MASK))
                {
                    _lastSafePoint = _actorTransform.position;
                    _accumulatedTime = 0.0f;
                }
            }
        }
    }
}