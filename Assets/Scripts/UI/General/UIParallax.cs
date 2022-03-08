using Sheldier.Common;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIParallax : MonoBehaviour, IUIElement
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField][Range(0.0f, 100.0f)] private float displaceFactor = 10f;
        [SerializeField] private float speed;
        private IInputProvider _inputProvider;
        
        public void Initialize(IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }
        public void OnActivated()
        {
        }

        public void OnDeactivated()
        {
        }

        public void Tick()
        {
            var direction = _inputProvider.CursorScreenCenterDirection.normalized * displaceFactor;
            rectTransform.localRotation = Quaternion.Slerp(rectTransform.rotation,Quaternion.Euler(-direction.y, -direction.x, 0.0f), Time.unscaledDeltaTime*speed);
        }

        public void Dispose()
        {
        }
    }
}