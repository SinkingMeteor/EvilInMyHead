using System;
using System.Collections;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Utilities;
using UnityEngine;
using Zenject;

namespace Sheldier.Actors.AI
{
    public class ActorsAIMover : MonoBehaviour, IExtraAIModule, ITickListener
    {
        [SerializeField] private Transform targetTransform;
        
        private PathProvider _pathProvider;
        private float _speed;
        private Vector2[] _paths;
        private int _targetIndex;
        
        private TickHandler _tickHandler;
        private AIInputProvider _aiInputProvider;
        private Transform _actorTransform;
        private Coroutine _followCoroutine;

        public void Initialize(ActorInternalData data, ActorsAIModule aiModule)
        {
            _actorTransform = data.Actor.transform;
            _tickHandler = data.TickHandler;
            _aiInputProvider = aiModule.AIInputProvider;
            _tickHandler.AddListener(this);
        }

        [Inject]
        private void InjectDependencies(PathProvider provider)
        {
            _pathProvider = provider;
        }
        public void Tick()
        {
            if (targetTransform == null || _paths != null) return;
            _pathProvider.RequestPath(_actorTransform.position, targetTransform.position, OnPathFound);
        }

        private void OnPathFound(Vector2[] waypoints, bool isSuccess)
        {
            if(!isSuccess) return;
            if(_followCoroutine != null)
                StopCoroutine(_followCoroutine);
            _paths = waypoints;
            _followCoroutine = StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            if (_paths.Length == 0)
            {
                _paths = null;
                yield break;
            }
            
            Vector2 currentWaypoint = _paths[0];

            while (true)
            {
                if (Vector2.Distance(_actorTransform.position.DiscardZ(), currentWaypoint) < 0.1f)
                {
                    _targetIndex++;
                    if (_targetIndex >= _paths.Length)
                    {
                        _paths = null;
                        _aiInputProvider.SetMovementDirection(Vector2.zero);
                        yield break;
                    }
                    currentWaypoint = _paths[_targetIndex];
                }

                var wayPointDirection = (currentWaypoint.AddZ() - _actorTransform.position).normalized;
                _aiInputProvider.SetMovementDirection(wayPointDirection);
                yield return null;
            }
            
        }

        private void OnDrawGizmos()
        {
            if(_paths == null) return;
            for (int i = _targetIndex; i < _paths.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_paths[i], 0.1f);

                if (i == _targetIndex)
                    Gizmos.DrawLine(_actorTransform.position, _paths[i]);
                else
                    Gizmos.DrawLine(_paths[i-1],_paths[i]);                       
            }
        }

        public void Dispose()
        {
            _tickHandler.RemoveListener(this);
        }


    }
}