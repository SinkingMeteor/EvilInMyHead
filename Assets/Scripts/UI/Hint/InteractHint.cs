using DG.Tweening;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class InteractHint : SerializedMonoBehaviour, IPoolObject<InteractHint>
    {
        private IPool<InteractHint> _pool;
        public Transform Transform => transform;

        [SerializeField] private Image interactImage;
        [SerializeField] private CanvasGroup canvasGroup;
        public void Initialize(IPool<InteractHint> pool)
        {
            _pool = pool;
        }

        public void SetImage(Sprite interactButtonSprite)
        {
            interactImage.sprite = interactButtonSprite;
        }
        
        public void OnInstantiated()
        {
            canvasGroup.DOFade(1.0f, 0.5f);
        }

        public async void Close()
        {
            await canvasGroup.DOFade(0.0f, 0.5f).AsyncWaitForCompletion();
            _pool.SetToPull(this);
        }
        public void Reset()
        {
            interactImage.sprite = null;
        }

        public void Dispose()
        {
        }
    }
}