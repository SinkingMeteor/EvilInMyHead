using System;
using System.Collections;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class UIHint : SerializedMonoBehaviour, IPoolObject<UIHint>, IFontRequier
    {
        public FontType FontTypeRequirer => FontType.DefaultPixelFont7;
        public Transform Transform => transform;

        [OdinSerialize] private IUIStateAnimationAppearing[] appearingAnimations;
            
        [SerializeField] private float delay;
        [SerializeField] private Image hintImageBackground;
        [SerializeField] private Image hintIcon;
        [SerializeField] private TextMeshProUGUI hintTitle;
            
        private IPool<UIHint> _pool;
        private Coroutine _waitCoroutine;
        private IFontProvider _fontProvider;

        public void Initialize(IPool<UIHint> pool)
        {
            for (int i = 0; i < appearingAnimations.Length; i++)
                appearingAnimations[i].Initialize();
            _pool = pool;
            hintTitle.font = _fontProvider.GetActualFont(FontTypeRequirer);
            _fontProvider.AddListener(this);
            
        }
        public void SetDependencies(IFontProvider fontProvider)
        {
            _fontProvider = fontProvider;
        }
        public void OnInstantiated()
        {
            for (int i = 0; i < appearingAnimations.Length; i++)
                appearingAnimations[i].PlayAnimation();
        }

        public void SetIconImage(Sprite icon)
        {
            hintIcon.sprite = icon;
            hintIcon.SetNativeSize();
        }
        public void UpdateFont(TMP_FontAsset textAsset)
        {
            hintTitle.font = textAsset;
        }
        public void SetTitle(string title)
        {
            hintTitle.text = title;
        }
        public void Deactivate()
        {
            _pool.SetToPull(this);
        }

        public void Reset()
        {
            StopWaiting();
            hintIcon.sprite = null;
        }

        public void WaitAndPerform(Action callback)
        {
            StopWaiting();
            _waitCoroutine = StartCoroutine(WaitBeforePerformActionCoroutine(callback));
        }

        public void StopWaiting()
        {
            if (_waitCoroutine != null)
            {
                hintImageBackground.fillAmount = 0.0f;
                StopCoroutine(_waitCoroutine);
            }
        }

        private IEnumerator WaitBeforePerformActionCoroutine(Action callback)
        {
            hintImageBackground.fillAmount = 0.0f;
            float currentTime = 0.0f;
            while (currentTime < delay)
            {
                currentTime += Time.unscaledDeltaTime;
                hintImageBackground.fillAmount = Mathf.InverseLerp(0.0f, delay, currentTime);
                yield return null;
            }

            hintImageBackground.fillAmount = 0.0f;
            callback();
        }

        public void Dispose()
        {
            _fontProvider.RemoveListener(this);
        }



    }
}