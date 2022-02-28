using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorsView : MonoBehaviour
    {
        public int CurrentSortingOrder => _currentSortingOrder;
        public Material CurrentBodyMaterial => body.sharedMaterial;

        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private SpriteRenderer shadow;
        
        private int _currentSortingOrder;

        public void SetMaterial(Material material)
        {
            body.sharedMaterial = material;
        }
        
        
        public void SetSortingOrder(int order)
        {
            body.sortingOrder = order;
            shadow.sortingOrder = order;
        }

        public void ResetSortingOrder()
        {
            body.sortingOrder = 0;
            shadow.sortingOrder = 0;
        }

        public void PlayAnimation(int animationID)
        {
            animator.Play(animationID);
        }
    }
}