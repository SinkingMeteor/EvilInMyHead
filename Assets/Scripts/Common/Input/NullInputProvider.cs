using UnityEngine;

namespace Sheldier.Common
{
    public class NullInputProvider : IInputProvider
    {
        public void Initialize()
        {
             
        }
        public Vector2 MovementDirection => Vector2.zero;
        public Vector2 CursorPosition => Vector2.down;

        private InputButton _useButton = new InputButton();

        public InputButton UseButton => _useButton;
    }
}