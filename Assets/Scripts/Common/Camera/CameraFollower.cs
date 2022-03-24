using System.Collections;
using UnityEngine;

namespace Sheldier.Common
{
    public class CameraFollower
    {
        private Transform _targetTransform;
        private Camera _camera;
        public Vector3 TargetPosition => _targetPosition;
        private Vector3 _targetPosition;
        private LateTickHandler _tickHandler;
        private Coroutine _smoothToTargetCoroutine;

        private ITargetProvider _currentTargetProvider;
        private DefaultTargetProvider _defaultTargetProvider;

        public void Initialize()
        {
            _defaultTargetProvider = new DefaultTargetProvider();
            _currentTargetProvider = _defaultTargetProvider;
        }
        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public void SetDependencies(LateTickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        public void SetNewTarget(Transform target)
        {
            _defaultTargetProvider.SetTarget(target);
            if(_smoothToTargetCoroutine != null)
                _tickHandler.StopCoroutine(_smoothToTargetCoroutine);
            _smoothToTargetCoroutine = _tickHandler.StartCoroutine(SmoothToNewTarget(target));
        }

        public void LateTick()
        {
            if (_targetTransform == null)
            {
                _targetPosition =  new Vector3(0.0f, 0.0f, _camera.transform.position.z);
                return;
            }

            var newPosition = _currentTargetProvider.GetTargetPosition();
           _targetPosition = new Vector3(newPosition.x, newPosition.y, _camera.transform.position.z);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _targetPosition, Time.deltaTime * 2.0f);
        }

        private IEnumerator SmoothToNewTarget(Transform newTarget)
        {
            if (_targetTransform == null)
            {
                _targetTransform = newTarget;
                yield break;
            }
            var lerpProvider =  new LerpTargetProvider();
            _currentTargetProvider = lerpProvider;
            float delta = 0.0f;
            while (delta < 1.0f)
            {
                delta += _tickHandler.TickDelta * 2.0f;
                delta = Mathf.Clamp01(delta);
                lerpProvider.SetLerpedPosition(Vector3.Lerp(_targetTransform.position, newTarget.transform.position, Mathf.SmoothStep(0.0f, 1.0f, delta)));
                yield return null;
            }
                
            _targetTransform = newTarget;
            _currentTargetProvider = _defaultTargetProvider;
        } 
        
        
        private interface ITargetProvider
        {
            Vector3 GetTargetPosition();
        }

        private class DefaultTargetProvider : ITargetProvider
        {
            private Transform _target;
            public void SetTarget(Transform target) => _target = target;
            public Vector3 GetTargetPosition() => _target.position;
        }
        private class LerpTargetProvider : ITargetProvider
        {
            private Vector3 _target;
            public void SetLerpedPosition(Vector3 target) => _target = target;
            public Vector3 GetTargetPosition() => _target;
        }
    }
}