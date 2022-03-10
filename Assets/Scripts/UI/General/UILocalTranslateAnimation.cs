using DG.Tweening;
using Sheldier.Common.Utilities;
using UnityEngine;

namespace Sheldier.UI
{
    public class UILocalTranslateAnimation : DOTweenUIAnimation, IUIStateAnimationAppearing
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector2 initialLocalPosition;
        [SerializeField] private Vector2 targetLocalPosition;
        public void Initialize()
        {
            
        }
        protected override Tween GetAnimation()
        {
            rectTransform.anchoredPosition = initialLocalPosition;
            return rectTransform.DOLocalMove(targetLocalPosition.AddZ(), duration);
        }
    }
}