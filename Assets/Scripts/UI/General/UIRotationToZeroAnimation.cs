using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class UIRotationToZeroAnimation : DOTweenUIAnimation, IUIStateAnimationAppearing
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float initialAngle;
        public void Initialize()
        {
            rectTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, initialAngle);
        }
        protected override Tween GetAnimation()
        {
            return rectTransform.DOLocalRotate(new Vector3(0.0f, 0.0f, 0.0f), duration);
        }

        
    }
}