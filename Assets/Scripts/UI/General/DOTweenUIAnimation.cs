using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.UI
{
    public abstract class DOTweenUIAnimation : SerializedMonoBehaviour
    {
        [SerializeField] [Range(0.1f, 4.0f)] protected float duration;
        [SerializeField] private Ease easeType;
        
        private Tween _mainSequence;

 
        protected abstract Tween GetAnimation();

        private async Task RunSequence()
        {
            KillSequence();

            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);
            sequence.SetEase(easeType);
            Tween animation = GetAnimation();
            sequence.Append(animation);
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