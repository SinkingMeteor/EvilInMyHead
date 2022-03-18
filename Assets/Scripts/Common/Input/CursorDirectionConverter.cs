using UnityEngine;

namespace Sheldier.Common
{
    public class CursorDirectionConverter
    {
        private Camera _camera;
        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }

        public Vector2 GetMouseScreenDirection(Vector2 mouseScreenPosition)
        {
            var screen = new Vector2(Screen.width, Screen.height);
            var halfScreen = screen * 0.5f;
            var directionMinusOneToOne = (mouseScreenPosition - halfScreen) / screen.y * 2.0f;
                
            return directionMinusOneToOne;
        }

        public Vector2 GetDirectionByTransform(Vector3 position, Vector2 cursorPosition)
        {
            var worldCursorPosition = _camera.ScreenToWorldPoint(cursorPosition);
            return (worldCursorPosition - position);
        }
    }
}