using Sheldier.Common;

namespace Sheldier.UI
{
    public interface IUIInitializable
    {
        void Initialize(IInventoryInputProvider inputProvider);
        void Dispose();
    }
}