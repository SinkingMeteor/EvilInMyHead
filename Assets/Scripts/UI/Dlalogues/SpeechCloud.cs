using System;
using System.Collections;
using System.Text;
using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Data;
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

        private Database<ActorStaticDialogueData> _staticDialogueDatabase;
        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private StringBuilder _stringBuilder;
        private IPool<SpeechCloud> _pool;
        private ISoundPlayer _soundPlayer;
        private Coroutine _typingCoroutine;
        private IFontProvider _fontProvider;


        public void Initialize(IPool<SpeechCloud> pool)
        {
            _pool = pool;
            appearingAnimation.Initialize();
            disappearingAnimation.Initialize();
            _stringBuilder = new StringBuilder();
            cloudTMP.font = _fontProvider.GetActualFont(FontTypeRequirer);
            _fontProvider.AddListener(this);

        }
        public void SetDependencies(ISoundPlayer soundPlayer, IFontProvider fontProvider, Database<ActorStaticDialogueData> dialogueDatabase, 
            Database<ActorDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _staticDialogueDatabase = dialogueDatabase;
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

            var typeName = _dynamicConfigDatabase.Get(actor.Guid).TypeName;
            ActorStaticDialogueData dynamicDialogueData = _staticDialogueDatabase.Get(typeName);
            cloudTMP.color = new Color(dynamicDialogueData.TextColorR, dynamicDialogueData.TextColorG, dynamicDialogueData.TextColorB);
            _typingCoroutine = StartCoroutine(TypeCoroutine(text, dynamicDialogueData.TypeSpeed));
        }
        public async void CloseCloud()
        {
            await disappearingAnimation.PlayAnimation();
            if(_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            _pool.SetToPull(this);
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
