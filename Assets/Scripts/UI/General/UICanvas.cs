using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.UI
{
    public class UICanvas : SerializedMonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Canvas canvas;
        
        
        public void OnActivated()
        {
            canvasGroup.blocksRaycasts = true;
        }

        public void OnDeactivated()
        {
            canvasGroup.blocksRaycasts = false;
        }

        public void SetSortingOrder(int order)
        {
            canvas.sortingOrder = order;
        }
    }
}