using Sheldier.Common;

namespace Sheldier.UI
{
    public interface ICursorRequirer
    {
        void SetCursor(ICursorProvider cursorProvider);
    }
}