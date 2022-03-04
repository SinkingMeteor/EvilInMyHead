using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.UI
{
    public abstract class DOTweenUIAnimation : SerializedMonoBehaviour, IUIStateAnimation
    {
        [SerializeField] [Range(0.1f, 4.0f)] protected float duration;
        [SerializeField] private Ease easeType;
        
        private Tween _mainSequence;

 
        protected abstract Tween[] GetAnimations();

        private async Task RunSequence()
        {
            KillSequence();

            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            sequence.SetEase(easeType);
            Tween[] animations = GetAnimations();
            for (int i = 0; i < animations.Length; i++)
            {
                sequence.Append(animations[i]);
            }

            _mainSequence = sequence;
            await _mainSequence.AsyncWaitForCompletion();
        }

        private void KillSequence() => _mainSequence?.Kill();

        public async Task PlayAnimation()
        {
            KillSequence();
            await RunSequence();
        }

        public void Reset()
        {
            KillSequence();
        }
    }
}