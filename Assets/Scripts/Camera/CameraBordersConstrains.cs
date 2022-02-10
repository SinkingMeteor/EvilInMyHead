using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Navigation
{
    public class CameraBordersConstrains : MonoBehaviour, ITickListener, IInitializable
    {
        private Vector2 _minCorner;
        private Vector2 _maxCorner;
        
        private TickHandler _tickHandler;
        private Camera _camera;

        [Inject]
        private void SetDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
            _camera = Camera.main;
        }
        
        public void Initialize()
        {
            _tickHandler.AddListener(this);
        }
        
        public bool Tick()
        {
            var height = _camera.orthographicSize;
            var width = height * _camera.aspect;
            _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, _minCorner.x + width, _maxCorner.x - width),
                Mathf.Clamp(_camera.transform.position.y, _minCorner.y + height, _maxCorner.y - height),
                _camera.transform.position.z);   
            
            return true;
        }
        
        #if UNITY_EDITOR
        private void SetCornersByChildObjects()
        {
            var childs = transform.childCount;
            if (childs < 1)
            {
                new GameObject("MinChild").transform.SetParent(transform);
                new GameObject("MaxChild").transform.SetParent(transform);
            }
                

            var minChild = transform.GetChild(0).position;
            _minCorner = new Vector2(minChild.x, minChild.y);

            var maxChild = transform.GetChild(1).position;
            _maxCorner = new Vector2(maxChild.x, maxChild.y);
        } 
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            SetCornersByChildObjects();

            Vector2 verticalLine = new Vector2(0, _maxCorner.y - _minCorner.y);
            Gizmos.DrawLine(_minCorner, _minCorner + verticalLine);
            Vector2 horizontalLine = new Vector2(_maxCorner.x - _minCorner.x, 0);
            Gizmos.DrawLine(_minCorner + horizontalLine, _minCorner);
            Gizmos.DrawLine(_maxCorner, _maxCorner - verticalLine);
            Gizmos.DrawLine(_maxCorner, _maxCorner - horizontalLine);
        }
        
        #endif


    }

}
