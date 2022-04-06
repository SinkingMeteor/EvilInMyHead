using System;

namespace Sheldier.UI
{
    public interface IUIItemSwitcher<T>
    {
        event Action<string> OnCurrentItemChanged;
    }
}