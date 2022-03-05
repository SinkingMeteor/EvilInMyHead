using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIFadeInAnimation : DOTweenUIAnimation, IUIStateAnimationAppearing
    {
        [SerializeField] private CanvasGroup canvasGroup;
        protected override Tween GetAnimation()
        {
            return canvasGroup.DOFade(1.0f, duration * (1.0f - canvasGroup.alpha));
        }
    }
}