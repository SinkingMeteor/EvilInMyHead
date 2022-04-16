using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorsView : MonoBehaviour
    {
        public SimpleAnimator Animator => animator;
        public Transform SpeechPoint => speechPoint;        
        public Material CurrentBodyMaterial => body.sharedMaterial;

        [SerializeField] private Transform speechPoint;
        [SerializeField] private SimpleAnimator animator;
        [SerializeField] private ActorAnimationCollection animationCollection;
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private SpriteRenderer shadow;
        private ActorStateDataModule _stateDataModule;

        private int _currentSortingOrder;
        private AnimationType _currentPlayingType = AnimationType.None;

        public void Initialize()
        {
            animator.Initialize();
        }
        public void SetDependencies(TickHandler tickHandler, ActorStateDataModule stateDataModule)
        {
            _stateDataModule = stateDataModule;
            animator.SetDependencies(tickHandler);
        }
        public void SetMaterial(Material material)
        {
            body.sharedMaterial = material;
        }

        public void Tick()
        {
            if(_stateDataModule.IsItemExists(GameplayConstants.FALL_STATE_DATA))
                shadow.enabled = !_stateDataModule.Get(GameplayConstants.FALL_STATE_DATA).StateValue;
        }
        public void SetSortingOrder(int order)
        {
            body.sortingOrder = order;
            shadow.sortingOrder = order;
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