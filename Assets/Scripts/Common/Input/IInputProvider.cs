using System;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Common
{
    public interface IInputProvider : IInitializable
    {
         Vector2 MovementDirection { get; }
         Vector2 CursorPosition { get; }
         InputButton UseButton { get; }
    }
}