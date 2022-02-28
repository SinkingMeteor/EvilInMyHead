using UnityEngine;

namespace Sheldier.Common.Level
{
    public interface ILevelElementSwitcher
    {
        void OnSwitch(Collider2D col);
    }
}