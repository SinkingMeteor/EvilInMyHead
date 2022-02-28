using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Common.Level
{
    public class FloorSwitcher : SerializedMonoBehaviour
    {
        [OdinSerialize] private ILevelElementSwitcher[] _switchers;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger)
                return;
            for (int i = 0; i < _switchers.Length; i++)
            {
                _switchers[i].OnSwitch(col);
            }
        }
    }
}