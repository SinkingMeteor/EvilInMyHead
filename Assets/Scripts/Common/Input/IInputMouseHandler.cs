using UnityEngine;

namespace Sheldier.Common
{
    public interface IInputMouseHandler
    {
        Vector3 GetScreenPointOfCursor(Vector2 mouseDirection);
        Vector2 GetMouseScreenDirection(Vector2 mousePosition);
    }
}