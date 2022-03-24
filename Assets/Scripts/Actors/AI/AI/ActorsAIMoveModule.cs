using System;
using System.Collections;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common.Pause;
using Sheldier.Common.Utilities;
using UnityEngine;

namespace Sheldier.Actors.AI
{
    public class ActorsAIMoveModule : IExtraActorModule, IPausable
    {
        private PathProvider _pathProvider;
        private Vector2[] _paths;
        private int _targetIndex;
        
        private Transform _actorTransform;
        private Coroutine _followCoroutine;

        private bool _isPaused;
        private PauseNotifier _pauseNotifier;
        private Actor _currentActor;
        private Action _onFinishedCallback;

        public void Initialize(ActorInternalData data)
        {
            _currentActor = data.Actor;
            _actorTransform = data.Actor.transform;
            _pauseNotifier.Add(this);
        }
        public void SetDependencies(PathProvider provider, PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
            _pathProvider = provider;
        }
        public void Pause() => _isPaused = true;
        public void Unpause() => _isPaused = false;

        public void MoveTo(Transform point, Action onFinished)
        {
            _onFinishedCallback = onFinished;
            _pathProvider.RequestPath(_actorTransform.position, point.position, OnPathFound);
        }
        private void OnPathFound(Vector2[] waypoints, bool isSuccess)
        {
            if (!isSuccess)
            {
                _onFinishedCallback?.Invoke();
                return;
            }
            if(_followCoroutine != null)
                _currentActor.StopCoroutine(_followCoroutine);
            _paths = waypoints;
            _followCoroutine = _currentActor.StartCoroutine(FollowPath());
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
                while (_isPaused)
                    yield return null;
                
                if (Vector2.Distance(_actorTransform.position.DiscardZ(), currentWaypoint) < 0.2f)
                {
                    _targetIndex++;
                    if (_targetIndex >= _paths.Length)
                    {
                        _paths = null;
                        _currentActor.InputController.SetMovementDirection(Vector2.zero);
                        _targetIndex = 0;
                        _onFinishedCallback?.Invoke();
                        yield break;
                    }
                    currentWaypoint = _paths[_targetIndex];
                }

                var wayPointDirection = (currentWaypoint.AddZ() - _actorTransform.position).normalized;
                _currentActor.InputController.SetMovementDirection(wayPointDirection);
                _currentActor.InputController.SetViewDirection(wayPointDirection);
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
            _pauseNotifier.Remove(this);
        }


    }
}