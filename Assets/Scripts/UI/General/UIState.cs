using Sheldier.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Zenject;

namespace Sheldier.UI
{
    public class UIState : SerializedMonoBehaviour, ITickListener
    {
        public bool IsVisible => _isVisible;

        [OdinSerialize] private IUIStateAnimation appearingAnimation;
        [OdinSerialize] private IUIStateAnimation disappearingAnimation;

        private bool _isVisible;

        public void Initialize()
        {
        }

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }

        public void Tick()
        {
            
        }

        public void Dispose()
        {
        }
    }
}