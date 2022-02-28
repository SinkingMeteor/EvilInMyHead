using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors.AI
{
    public class AIInputProvider : IInputProvider
    {

        public Vector2 MovementDirection => _movementDirection;
        public Vector2 CursorScreenCenterDirection => _cursorScreenCenterDirection;
        public InputButton UseButton => _useButton;
        public InputButton AttackButton => _attackButton;
        public InputButton ReloadButton => _reloadButton;
        
        private InputButton _useButton;
        private InputButton _attackButton;
        private InputButton _reloadButton;
        private Vector2 _movementDirection;
        private Vector2 _cursorScreenCenterDirection;

        public void Initialize()
        {
            _useButton = new InputButton();
            _reloadButton = new InputButton();
            _attackButton = new InputButton();
            _movementDirection = Vector2.zero;
            _cursorScreenCenterDirection = Vector2.zero;
        }

        public Vector2 GetNonNormalizedDirectionToCursorFromPosition(Vector3 position)
        {
            return Vector2.zero;
            
        }

        public void SetMovementDirection(Vector2 movementDirection) => _movementDirection = movementDirection;
    }
}