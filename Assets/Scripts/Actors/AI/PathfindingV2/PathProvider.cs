using System;
using System.Collections.Generic;
using Sheldier.Common.Utilities;
using UnityEngine;
using Zenject;

namespace Sheldier.Actors.Pathfinding
{
    public class PathProvider
    {
        private Queue<PathRequest> _requestsQueue;
        private PathRequest _currentRequest;
        private bool _isProcessingPath;
        private Pathfinder _pathfinder;

        public void Initialize()
        {
            _requestsQueue = new Queue<PathRequest>();
        }

        public void SetDependencies(Pathfinder pathfinder)
        {
            _pathfinder = pathfinder;
        }
        
        public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector2[], bool> callBack)
        {
            PathRequest request = new PathRequest(pathStart, pathEnd, callBack);
            _requestsQueue.Enqueue(request);
            TryProcessNext();
        }

        private void FinishedProcessingPath(Vector2[] path, bool isSuccess)
        {
            _currentRequest.CallBack(path, isSuccess);
            _isProcessingPath = false;
            TryProcessNext();
        }
        private void TryProcessNext()
        {
            if (!_isProcessingPath && _requestsQueue.Count > 0)
            {
                _currentRequest = _requestsQueue.Dequeue();
                _isProcessingPath = true;
                _pathfinder.FindPath(_currentRequest.PathStart, _currentRequest.PathEnd, FinishedProcessingPath);
            }
        }

        private struct PathRequest
        {
            public Vector2 PathStart => _pathStart;
            public Vector2 PathEnd => _pathEnd;
            public Action<Vector2[], bool> CallBack => _callBack;

            private Action<Vector2[], bool> _callBack;
            private Vector2 _pathStart;
            private Vector2 _pathEnd;

            public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector2[], bool> callBack)
            {
                _pathStart = pathStart.DiscardZ();
                _pathEnd = pathEnd.DiscardZ();
                _callBack = callBack;
            }
        }
    }
}