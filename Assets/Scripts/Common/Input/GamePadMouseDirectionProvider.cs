using UnityEngine;

namespace Sheldier.Common
{
    public class GamePadMouseDirectionProvider : IInputMouseHandler
    {

        private Vector2 previousDirection;

        public Vector3 GetScreenPointOfCursor(Vector2 mouseDirection)
        {
            return (mouseDirection * 0.5f + new Vector2(0.5f, 0.5f)) * new Vector2(Screen.width, Screen.height);
        }
        public Vector2 GetMouseScreenDirection(Vector2 mouseDirection)
        {
            var newDirection = Vector2.Lerp(previousDirection, mouseDirection, Time.deltaTime * 5.0f);
            previousDirection = newDirection;
            return newDirection;
        }
    }
}