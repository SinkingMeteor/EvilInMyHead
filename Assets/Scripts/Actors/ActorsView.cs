using Sheldier.Common.Animation;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorsView : MonoBehaviour
    {
        public int CurrentSortingOrder => _currentSortingOrder;
        public Material CurrentBodyMaterial => body.sharedMaterial;

        [SerializeField] private SimpleAnimator animator;
        [SerializeField] private ActorAnimationCollection animationCollection;
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private SpriteRenderer shadow;
        
        private int _currentSortingOrder;
        private AnimationType _currentPlayingType = AnimationType.None;

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
        
        public void PlayAnimation(AnimationType animationType)
        {
            if(_currentPlayingType == animationType)
                return;
            _currentPlayingType = animationType;
            AnimationData data = animationCollection.AnimationCollection[animationType];
            if(animator.IsPlaying)
                animator.StopPlaying();
            animator.Play(data);
        }

        public void SetActorAppearance(ActorAnimationCollection appearance)
        {
            animationCollection = appearance;
        }

        public void Dispose()
        {
            animator.Dispose();
        }
    }
}