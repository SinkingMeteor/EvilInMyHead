using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Sheldier.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public async Task FadeIn(float duration = 1.0f, Ease ease = Ease.InOutCubic)
        {
            canvasGroup.blocksRaycasts = true;
            var fadeTween = canvasGroup.DOFade(1.0f, duration);
            fadeTween.SetEase(ease);
            await fadeTween.AsyncWaitForCompletion();
        }
        
        public async Task FadeOut(float duration = 1.0f, Ease ease = Ease.InOutCubic)
        {
            var fadeTween = canvasGroup.DOFade(0.0f, duration);
            fadeTween.SetEase(ease);
            await fadeTween.AsyncWaitForCompletion();
            canvasGroup.blocksRaycasts = false;
        }
    }
}
