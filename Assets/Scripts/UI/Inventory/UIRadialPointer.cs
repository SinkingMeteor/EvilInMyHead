using Sheldier.Common;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sheldier.UI
{
    public class UIRadialPointer : MonoBehaviour, IUIElement
    {
        public int CurrentSegment => _currentSegment;
        
        [SerializeField] private RectTransform pointerBaseRectTransform;
        [SerializeField] private Image pointerImage;
        [SerializeField] private float inactiveRadius;
        [SerializeField] private float deltaMaxDistance;
        
        private int _currentSegment;
        private int _totalSegments;
        private float _anglePerSegment;
        
        private IInputProvider _inputProvider;

        private const float TAU = 6.2832f;

        public void Initialize()
        {
        }

        [Inject]
        private void InjectDependencies(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }

        public void SetSegments(int segments)
        {
            _totalSegments = segments;
            _anglePerSegment = TAU / _totalSegments;
        }
        public void OnActivated()
        {
        }

        public void OnDeactivated()
        {
        }

        public void Tick()
        {
            Vector2 dir = _inputProvider.CursorScreenCenterDirection;

            var angle = (Mathf.Atan2(dir.y, dir.x));
            pointerBaseRectTransform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg), 0.5f);
            angle += Mathf.PI;
            _currentSegment = Mathf.FloorToInt(angle / (_anglePerSegment + 0.1f));
            if (!CheckCursorLength(dir))
                _currentSegment = -1;
        }

        private bool CheckCursorLength(Vector2 dir)
        {
            float magnitude = dir.magnitude;
            float delta = magnitude - inactiveRadius;
            float colorValue = Mathf.Clamp01(Mathf.InverseLerp(0.0f, deltaMaxDistance, delta));
            pointerImage.color = new Color(colorValue, colorValue, colorValue, colorValue);
            if (delta < 0) return false;
            return true;
        }

        public void Dispose()
        {
        }

        private void OnDrawGizmos()
        {
            if (pointerBaseRectTransform == null) return;
            RectTransform parent = pointerBaseRectTransform.parent as RectTransform;
            Gizmos.matrix = parent.localToWorldMatrix;
            Gizmos.DrawWireSphere(parent.anchoredPosition, inactiveRadius);
            for (int i = 0; i < _totalSegments; i++)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawLine(parent.anchoredPosition,
                    parent.anchoredPosition + i.UnitVectorFromSegment(_totalSegments)*60.0f);
                
                Gizmos.color = Color.green;
                var angle = (i * _anglePerSegment);
                var y = Mathf.Sin(angle);
                var x = Mathf.Cos(angle);
                Gizmos.DrawLine(parent.anchoredPosition, parent.anchoredPosition + new Vector2(x,y)*80.0f);
            }
        }

    }
}