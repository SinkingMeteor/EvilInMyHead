using UnityEngine;

namespace Sheldier.Common
{
    public class NullInputProvider : IInputProvider
    {
        public void Initialize()
        {
             
        }
        public Vector2 MovementDirection => Vector2.zero;
        public Vector2 CursorScreenDirection => Vector2.zero;
        public InputButton UseButton => _useButton;
        public InputButton AttackButton => _attackButton;
        public InputButton ReloadButton => _reloadButton;

        private InputButton _reloadButton = new InputButton();
        private InputButton _useButton = new InputButton();
        private InputButton _attackButton = new InputButton();
    }
}