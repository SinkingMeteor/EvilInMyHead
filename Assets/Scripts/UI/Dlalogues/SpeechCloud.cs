using System;
using System.Text;
using Sheldier.Common.Audio;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace Sheldier.UI
{
    public class SpeechCloud : SerializedMonoBehaviour, IPoolObject<SpeechCloud>
    {
        public Transform Transform => transform;
    
        [SerializeField] private TextMeshProUGUI cloudTMP;
        [SerializeField] private RectTransform rectTransform;
        [OdinSerialize] private IUIStateAnimationAppearing appearingAnimation;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;

        private StringBuilder _stringBuilder;
        private IPoolSetter<SpeechCloud> _poolSetter;
        private ISoundPlayer _soundPlayer;


        public void Initialize(IPoolSetter<SpeechCloud> poolSetter)
        {
            _poolSetter = poolSetter;
            appearingAnimation.Initialize();
            disappearingAnimation.Initialize();
        }
        public void SetDependencies(ISoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
        }
        public void OnInstantiated()
        {
            appearingAnimation.PlayAnimation();
        }

        public void Add(char letter)
        {
            _stringBuilder.Append(letter);
            cloudTMP.text = _stringBuilder.ToString();
        }
    
        public async void CloseCloud()
        {
            await disappearingAnimation.PlayAnimation();
            _poolSetter.SetToPull(this);
        }
        public void Reset()
        {
            _stringBuilder.Clear();
            cloudTMP.text = String.Empty;
        }
    }
}
