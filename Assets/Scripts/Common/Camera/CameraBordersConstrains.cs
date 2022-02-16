using UnityEngine;

namespace Sheldier.Common
{
    public class CameraBordersConstrains : MonoBehaviour
    {
        [SerializeField] private Vector2 _minCorner;
        [SerializeField] private Vector2 _maxCorner;
        
        private Camera _camera;

        
        public void SetDependencies(Camera camera)
        {
            _camera = camera;
        }

        public void LateTick()
        {
            var height = _camera.orthographicSize;
            var width = height * _camera.aspect;
            _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, _minCorner.x + width, _maxCorner.x - width),
                Mathf.Clamp(_camera.transform.position.y, _minCorner.y + height, _maxCorner.y - height),
                _camera.transform.position.z);   
            
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector2 verticalLine = new Vector2(0, _maxCorner.y - _minCorner.y);
            Gizmos.DrawLine(_minCorner, _minCorner + verticalLine);
            Vector2 horizontalLine = new Vector2(_maxCorner.x - _minCorner.x, 0);
            Gizmos.DrawLine(_minCorner + horizontalLine, _minCorner);
            Gizmos.DrawLine(_maxCorner, _maxCorner - verticalLine);
            Gizmos.DrawLine(_maxCorner, _maxCorner - horizontalLine);


            if (_camera == null)
                return;
            Gizmos.DrawLine(_camera.transform.position,
                _camera.transform.position + new Vector3(_camera.orthographicSize * _camera.aspect,
                    _camera.orthographicSize, 0.0f));
        }
        
        #endif


    }
}
