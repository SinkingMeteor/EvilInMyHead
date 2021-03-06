using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIFadeOutAnimation : DOTweenUIAnimation, IUIStateAnimationDisappearing
    {
        [SerializeField] private CanvasGroup canvasGroup;
        public void Initialize()
        {
            
        }
        protected override Tween GetAnimation()
        {
            return canvasGroup.DOFade(0.0f, duration * canvasGroup.alpha);
        }
    }
}