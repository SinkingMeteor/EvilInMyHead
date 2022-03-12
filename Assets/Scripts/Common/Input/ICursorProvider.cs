using UnityEngine;

namespace Sheldier.Common
{
    public interface ICursorProvider
    {
        Vector2 CursorScreenCenterDirection { get; }
    }
}