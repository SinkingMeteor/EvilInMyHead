using Sheldier.Actors;
using UnityEngine;

namespace Sheldier.Common.Level
{
    public class ActorsSortingOrderSwitcher : MonoBehaviour, ILevelElementSwitcher
    {
        [SerializeField] private int newSortingOrder;
        public void OnSwitch(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out ActorsView view))
            {
                view.SetSortingOrder(newSortingOrder);
            }
        }
    }
}