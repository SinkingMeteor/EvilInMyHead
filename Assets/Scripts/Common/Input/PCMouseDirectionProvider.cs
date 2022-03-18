using UnityEngine;

namespace Sheldier.Common
{
    public class PCMouseDirectionProvider : IInputMouseHandler
    {
        private readonly CursorDirectionConverter _cursorDirectionConverter;

        public PCMouseDirectionProvider(CursorDirectionConverter cursorDirectionConverter)
        {
            _cursorDirectionConverter = cursorDirectionConverter;
        }

        public Vector3 GetScreenPointOfCursor(Vector2 mouseDirection)
        {
            return mouseDirection;
        }

        public Vector2 GetMouseScreenDirection(Vector2 mousePosition)
        {
            return _cursorDirectionConverter.GetMouseScreenDirection(mousePosition);
        }
    }
}