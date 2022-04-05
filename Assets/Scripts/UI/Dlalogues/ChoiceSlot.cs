using System.Threading.Tasks;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class ChoiceSlot : SerializedMonoBehaviour, IPoolObject<ChoiceSlot>, IFontRequier
    {
        public IDialogueReplica Next => _nextReplica;
        public Transform Transform => transform;
        public FontType FontTypeRequirer => FontType.DefaultPixelFont7;

        
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private Image inputActionIcon;

        [OdinSerialize] private IUIStateAnimationAppearing[] appearingAnimations;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;
        [OdinSerialize] private IUIStateAnimationAppearing selectAnimation;
        
        private IDialogueReplica _nextReplica;
        private IFontProvider _fontProvider;
        private IPool<ChoiceSlot> _pool;

        public void Initialize(IPool<ChoiceSlot> pool)
        {
            _pool = pool;
            _fontProvider.AddListener(this);
            titleTMP.font = _fontProvider.GetActualFont(FontTypeRequirer);
        }

        public void SetDependencies(IFontProvider fontProvider)
        {
            _fontProvider = fontProvider;
        }
        public void OnInstantiated()
        {
        }

        public void SetText(string title) => titleTMP.text = title;

        public void SetBindIcon(Sprite icon)
        {
            inputActionIcon.sprite = icon;
        }
        public void Activate(ReplicaNode next)
        {
            _nextReplica = next;
            for (int i = 0; i < appearingAnimations.Length; i++)
                appearingAnimations[i].PlayAnimation();
        }
        public async Task Select()
        {
            await selectAnimation.PlayAnimation();
        }
        public async Task Deactivate()
        {
            await disappearingAnimation.PlayAnimation();
            _pool.SetToPull(this);
        }

        public void Reset()
        {
            _nextReplica = null;
        }
        public void UpdateFont(TMP_FontAsset textAsset)
        {
            titleTMP.font = textAsset;
        }
        public void Dispose()
        {
            _fontProvider.RemoveListener(this);

        }


    }
}