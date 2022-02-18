using System;
using UnityEngine;

namespace Sheldier.Actors
{
    public interface IActorsHand
    {
        event Action<Vector2> OnHandAttack;
        event Action OnHandReload;
        event Action OnHandUse;
    }
}