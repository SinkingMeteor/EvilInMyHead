using UnityEngine;

namespace Sheldier.Common.Utilities
{
    public static class MathExtensions
    {
        public static Vector2 DiscardZ(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);
        public static Vector3 AddZ(this Vector2 vector2) => new Vector3(vector2.x, vector2.y, 0.0f);
    }
}