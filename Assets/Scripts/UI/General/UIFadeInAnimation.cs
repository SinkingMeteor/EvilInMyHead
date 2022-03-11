using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIFadeInAnimation : DOTweenUIAnimation, IUIStateAnimationAppearing
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float initialAlpha = 0.0f;
        public void Initialize()
        {
            canvasGroup.alpha = 0.0f;
        }
        protected override Tween GetAnimation()
        {
            canvasGroup.alpha = initialAlpha;
            return canvasGroup.DOFade(1.0f, duration * (1.0f - canvasGroup.alpha));
        }
    }
}