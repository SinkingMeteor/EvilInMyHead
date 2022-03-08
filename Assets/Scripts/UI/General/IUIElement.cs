using Sheldier.Common;

namespace Sheldier.UI
{
    public interface IUIElement
    {
        void Initialize(IInputProvider inputProvider);
        void OnActivated();
        void OnDeactivated();
        void Tick();

        void Dispose();
    }
}