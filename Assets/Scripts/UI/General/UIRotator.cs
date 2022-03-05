using UnityEngine;

namespace Sheldier.UI
{
    public class UIRotator : MonoBehaviour, IVisualUIElement
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float speed;
        [SerializeField] private bool clockwise;

        private float _currentDirection;
        private float _currentSpeed;
        
        public void Initialize()
        {
            _currentDirection = clockwise ? 1.0f : -1.0f;
        }

        public void Activate()
        {
            _currentSpeed = speed;
        }

        public void Deactivate()
        {
            _currentSpeed = 0;
        }

        public void Tick()
        {
            rectTransform.Rotate(new Vector3(0.0f, 0.0f, 1.0f * _currentDirection) * _currentSpeed * Time.unscaledDeltaTime, Space.Self);
        }

        public void Dispose()
        {
            
        }
    }
}