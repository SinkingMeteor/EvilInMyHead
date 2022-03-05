using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIScaleUpAnimation : DOTweenUIAnimation, IUIStateAnimationAppearing
    {
        [SerializeField] private RectTransform rectTransform;
        protected override Tween GetAnimation()
        {
            rectTransform.localScale = Vector3.zero;
            return rectTransform.DOScale(Vector3.one, duration);
        }
    }
}