using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIScaleDownAnimation : DOTweenUIAnimation, IUIStateAnimationDisappearing
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector3 targetScale;
        public void Initialize()
        {
            
        }
        protected override Tween GetAnimation()
        {
            return rectTransform.DOScale(targetScale, duration);
        }
    }
}