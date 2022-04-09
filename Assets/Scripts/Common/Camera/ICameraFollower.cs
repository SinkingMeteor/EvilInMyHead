using UnityEngine;

namespace Sheldier.Common
{
    public interface ICameraFollower
    {
        void SetFollowTarget(Transform transform);
    }
}