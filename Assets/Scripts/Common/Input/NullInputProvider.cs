using Sheldier.Common.Utilities;
using UnityEngine;

namespace Sheldier.Common
{
    public class NullInputProvider : IGameplayInputProvider
    {
        public Vector2 MovementDirection => _movementDirection;
        public Vector2 CursorScreenCenterDirection => _viewDirection;
        public InputButton UseButton => _useButton;
        public InputButton AttackButton => _attackButton;
        public InputButton ReloadButton => _reloadButton;
        public InputButton JumpButton => _jumpButton;
        public InputButton OpenInventoryButton => _openInventoryButton;
        public Vector2 GetNonNormalizedDirectionToCursorFromPosition(Vector3 position) => _viewDirection;

        private InputButton _reloadButton;
        private InputButton _useButton;
        private InputButton _attackButton;
        private InputButton _openInventoryButton;
        private InputButton _jumpButton;
        private Vector2 _movementDirection;
        private Vector2 _viewDirection;

        public void Initialize()
        {
            _reloadButton = new InputButton();
            _useButton = new InputButton();
            _attackButton = new InputButton();
            _openInventoryButton = new InputButton();
            _jumpButton = new InputButton();
            _movementDirection = Vector2.zero;
            _viewDirection = Vector2.zero;
        }
        
        public void SwitchActionMap(ActionMapType actionMapType)
        { }
        public void SetMovementDirection(Vector2 movementDirection) => _movementDirection = movementDirection;
        public void SetViewDirection(Vector2 viewDirection) => _viewDirection = viewDirection;
    }
}