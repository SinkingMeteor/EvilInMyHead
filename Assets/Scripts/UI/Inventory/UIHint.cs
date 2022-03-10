using System;
using System.Collections;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class UIHint : SerializedMonoBehaviour, IPoolObject<UIHint>
    {
        public Transform Transform => transform;

        [OdinSerialize] private IUIStateAnimationAppearing appearingAnimation;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;
            
        [SerializeField] private float delay;
        [SerializeField] private Image hintImageBackground;
        [SerializeField] private Image hintIcon;
        [SerializeField] private TextMeshProUGUI hintTitle;
            
        private IPoolSetter<UIHint> _poolSetter;
        private Coroutine _waitCoroutine;

        public void Initialize(IPoolSetter<UIHint> poolSetter)
        {
            appearingAnimation.Initialize();
            disappearingAnimation.Initialize();
            _poolSetter = poolSetter;
        }

        public void OnInstantiated()
        {
            appearingAnimation.PlayAnimation();
        }

        public void SetIconImage(Sprite icon)
        {
            hintIcon.sprite = icon;
        }

        public void SetTitle(string title)
        {
            hintTitle.text = title;
        }
        public async void Deactivate()
        {
            await disappearingAnimation.PlayAnimation();
            _poolSetter.SetToPull(this);
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
            if(_waitCoroutine != null)
                StopCoroutine(_waitCoroutine);
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

    }
}