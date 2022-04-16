using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Item
{
    public class MoveObject : SerializedMonoBehaviour
    {
        [OdinSerialize] private IMoveObjectPoint[] movePoints;

        public void EnableAllExcept(IMoveObjectPoint moveObjectPoint)
        {
            SetActiveExcept(true, moveObjectPoint);
        }

        public void DisableAllExcept(IMoveObjectPoint moveObjectPoint)
        {
            SetActiveExcept(false, moveObjectPoint);
        }

        private void SetActiveExcept(bool isActive, IMoveObjectPoint moveObjectPoint)
        {
            for (int i = 0; i < movePoints.Length; i++)
            {
                if(ReferenceEquals(movePoints[i], moveObjectPoint))
                    continue;
                moveObjectPoint.GameObject.SetActive(isActive);
            }
        }
        
    }
}