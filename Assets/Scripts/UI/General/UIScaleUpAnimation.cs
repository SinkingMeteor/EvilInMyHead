using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIScaleUpAnimation : DOTweenUIAnimation, IUIStateAnimationAppearing
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector3 initialScale;
        [SerializeField] private Vector3 targetScale = Vector3.one;
        public void Initialize()
        {
            rectTransform.localScale = initialScale;
        }
        protected override Tween GetAnimation()
        {
            return rectTransform.DOScale(targetScale, duration);
        }
    }
}