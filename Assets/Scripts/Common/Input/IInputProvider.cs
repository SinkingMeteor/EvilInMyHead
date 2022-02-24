using System;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Common
{
    public interface IInputProvider : IInitializable
    {
         Vector2 MovementDirection { get; }
         Vector2 CursorScreenCenterDirection { get; }
         InputButton UseButton { get; }
         InputButton AttackButton { get; }
         InputButton ReloadButton { get; }
         Vector2 GetNormalizedDirectionToCursorFromPosition(Vector3 position);
    }
}