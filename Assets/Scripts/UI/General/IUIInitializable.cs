using Sheldier.Common;

namespace Sheldier.UI
{
    public interface IUIInitializable
    {
        void Initialize(IUIInputProvider inputProvider);
        void Dispose();
    }
}