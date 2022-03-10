using Sheldier.Common;

namespace Sheldier.UI
{
    public interface IUIInitializable
    {
        void Initialize(IInputProvider inputProvider);
        void Dispose();
    }
}