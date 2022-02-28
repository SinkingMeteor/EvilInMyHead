using System;
using UnityEngine;

namespace Sheldier.Common.Level
{
    public class ColliderSwitcher : MonoBehaviour, ILevelElementSwitcher
    {
        [SerializeField] private Transform collidersToHide;
        [SerializeField] private Transform collidersToShow;

        public void OnSwitch(Collider2D col)
        {
            collidersToHide.gameObject.SetActive(false);
            collidersToShow.gameObject.SetActive(true);
        }
    }
}

