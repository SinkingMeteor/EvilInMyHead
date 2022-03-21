using UnityEngine;

namespace Sheldier.Common
{
    public interface IGameplayInputProvider : ICursorProvider
    {
         Vector2 MovementDirection { get; }
         InputButton UseButton { get; }
         InputButton AttackButton { get; }
         InputButton ReloadButton { get; }
         Vector2 GetNonNormalizedDirectionToCursorFromPosition(Vector3 position);
         void SwitchActionMap(ActionMapType actionMapType);
         void SetMovementDirection(Vector2 movementDirection);
         void SetViewDirection(Vector2 viewDirection);
    }
}