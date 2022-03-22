using System;
using System.Collections;
using System.Text;
using Sheldier.Actors;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace Sheldier.UI
{
    public class SpeechCloud : SerializedMonoBehaviour, IPoolObject<SpeechCloud>, IFontRequier
    {
        public FontType FontTypeRequirer => FontType.DefaultPixelFont7;
        public Transform Transform => transform;

        [SerializeField] private RectTransform pointerRect;
        [SerializeField] private RectTransform backgroundRect;
        [SerializeField] private TextMeshProUGUI cloudTMP;
        [SerializeField] private SpeechRectResizer rectResizer;
        [OdinSerialize] private IUIStateAnimationAppearing appearingAnimation;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;

        private StringBuilder _stringBuilder;
        private IPoolSetter<SpeechCloud> _poolSetter;
        private ISoundPlayer _soundPlayer;
        private Coroutine _typingCoroutine;
        private IFontProvider _fontProvider;


        public void Initialize(IPoolSetter<SpeechCloud> poolSetter)
        {
            _poolSetter = poolSetter;
            appearingAnimation.Initialize();
            disappearingAnimation.Initialize();
            _stringBuilder = new StringBuilder();
            cloudTMP.font = _fontProvider.GetActualFont(FontTypeRequirer);
            _fontProvider.AddListener(this);

        }
        public void SetDependencies(ISoundPlayer soundPlayer, IFontProvider fontProvider)
        {
            _soundPlayer = soundPlayer;
            _fontProvider = fontProvider;
        }
        public void OnInstantiated()
        {
            appearingAnimation.PlayAnimation();
        }

        public void UpdateFont(TMP_FontAsset textAsset) => cloudTMP.font = textAsset;

        public void SetText(string text, Actor actor)
        {
            rectResizer.Resize(text);
            float sizeY =  backgroundRect.rect.height;
            transform.position += new Vector3(0.0f, sizeY * 0.5f, 0.0f);
            pointerRect.position = new Vector3(actor.transform.position.x, pointerRect.position.y, pointerRect.position.z);
            cloudTMP.text = text;
            cloudTMP.color = actor.DataModule.DialogueDataModule.TypeColor;

            _typingCoroutine = StartCoroutine(TypeCoroutine(text, actor.DataModule.DialogueDataModule.TypeSpeed));
        }
        public async void CloseCloud()
        {
            await disappearingAnimation.PlayAnimation();
            if(_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            _poolSetter.SetToPull(this);
        }
        public void Reset()
        {
            _stringBuilder.Clear();
            cloudTMP.color = Color.white;
            cloudTMP.text = String.Empty;
        }

        private void Add(char letter)
        {
            _stringBuilder.Append(letter);
            cloudTMP.text = _stringBuilder.ToString();
        }
        private IEnumerator TypeCoroutine(string text, float typeSpeed)
        {
            var delay = new WaitForSeconds(typeSpeed);
            char[] textArr = text.ToCharArray();
            for (int i = 0; i < textArr.Length; i++)
            {
                Add(textArr[i]);
                yield return delay;
            }
        }
        public void Dispose()
        {
            _fontProvider.RemoveListener(this);
        }
    }
}
