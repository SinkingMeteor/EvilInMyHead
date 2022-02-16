using UnityEngine;
using UnityEngine.InputSystem.DualShock;

namespace Sheldier.Common
{
    public class NullInputProvider : IInputProvider
    {
        public void Initialize()
        {
             
        }
        public Vector2 MovementDirection => Vector2.zero;
        public Vector2 CursorWorldPosition => Vector2.down;
        public Vector2 CursorScreenDirection => Vector2.zero;

        private InputButton _useButton = new InputButton();

        public InputButton UseButton => _useButton;
    }
}