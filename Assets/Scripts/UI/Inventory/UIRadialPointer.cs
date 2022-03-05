using System;
using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class UIRadialPointer : MonoBehaviour, IVisualUIElement
    {
        public int CurrentSegment => _currentSegment;
        
        [SerializeField] private RectTransform pointerBaseRectTransform;
        
        private int _currentSegment;
        private int _totalSegments;
        private float _anglePerSegment;
        
        private IInputProvider _inputProvider;

        private const float TAU = 6.28f;

        public void Initialize()
        {
            SetSegments(3);
        }

        [Inject]
        private void InjectDependencies(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        public void SetSegments(int segments)
        {
            _totalSegments = segments;
            _anglePerSegment = (TAU / _totalSegments) * Mathf.Rad2Deg;
        }
        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void Tick()
        {
            Vector2 dir = _inputProvider.CursorScreenCenterDirection;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            pointerBaseRectTransform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f, 0f, angle), 0.5f);

            _currentSegment = (int) (angle / _anglePerSegment);
            Debug.Log(_currentSegment);
        }

        public void Dispose()
        {
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}