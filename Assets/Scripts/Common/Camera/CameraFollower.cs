using UnityEngine;

namespace Sheldier.Common
{
    public class CameraFollower
    {
        private Transform _targetTransform;
        private Camera _camera;
        public Vector3 TargetPosition => _targetPosition;
        private Vector3 _targetPosition;
        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }
        
        public void SetNewTarget(Transform target)
        {
            _targetTransform = target;
        }

        public void LateTick()
        {
            if (_targetTransform == null)
            {
                _targetPosition =  new Vector3(0.0f, 0.0f, _camera.transform.position.z);
                return;
            }

            _targetPosition = new Vector3(_targetTransform.position.x, _targetTransform.position.y, _camera.transform.position.z);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _targetPosition, Time.deltaTime * 2.0f);
        }
    }
}