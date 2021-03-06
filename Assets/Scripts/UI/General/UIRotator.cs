using Sheldier.Common;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIRotator : MonoBehaviour, IUIActivatable, ITickListener
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float speed;
        [SerializeField] private bool clockwise;

        private float _currentDirection;
        private float _currentSpeed;

        public void OnActivated()
        {
            _currentDirection = clockwise ? 1.0f : -1.0f;
            _currentSpeed = speed;
        }

        public void OnDeactivated()
        {
            _currentSpeed = 0;
        }

        public void Tick()
        {
            rectTransform.Rotate(new Vector3(0.0f, 0.0f, 1.0f * _currentDirection) * _currentSpeed * Time.unscaledDeltaTime, Space.Self);
        }

    }
}