using Sheldier.Actors;
using UnityEngine;

namespace Sheldier.Common
{
    public static class Extensions
    {
        public static ActorDirectionView GetViewDirection(this Vector2 mousePosition)
        {
            var dotProduct = Vector2.Dot(Vector2.up, mousePosition);
            if (dotProduct < -0.7f)
                return ActorDirectionView.Front;
            if (dotProduct < 0.2f)
                return ActorDirectionView.FrontSide;
            if (dotProduct < 0.6f)
                return ActorDirectionView.BackSide;
            else
                return ActorDirectionView.Back;
        }
    }

}
