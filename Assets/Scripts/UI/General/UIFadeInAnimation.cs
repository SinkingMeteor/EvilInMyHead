using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIFadeInAnimation : DOTweenUIAnimation
    {
        [SerializeField] private CanvasGroup canvasGroup;
        protected override Tween[] GetAnimations()
        {
            return new Tween[] {canvasGroup.DOFade(1.0f, duration * (1.0f - canvasGroup.alpha))};
        }
    }
}