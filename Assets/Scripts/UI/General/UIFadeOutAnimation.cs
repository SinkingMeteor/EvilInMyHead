using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIFadeOutAnimation : DOTweenUIAnimation
    {
        [SerializeField] private CanvasGroup canvasGroup;
        protected override Tween[] GetAnimations()
        {
            return new Tween[] {canvasGroup.DOFade(0.0f, duration * canvasGroup.alpha)};
        }
    }
}