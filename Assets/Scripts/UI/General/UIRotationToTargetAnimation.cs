using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIRotationToTargetAnimation : DOTweenUIAnimation, IUIStateAnimationDisappearing
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float targetAngle;
        public void Initialize()
        {
        }
        protected override Tween GetAnimation()
        {
            return rectTransform.DOLocalRotate(new Vector3(0.0f, 0.0f, targetAngle), duration);
        }

        
    }
}