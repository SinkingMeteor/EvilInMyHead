using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIScaleDownAnimation : DOTweenUIAnimation, IUIStateAnimationDisappearing
    {
        [SerializeField] private RectTransform rectTransform;
        protected override Tween GetAnimation()
        {
            return rectTransform.DOScale(Vector3.zero, duration);
        }
    }
}