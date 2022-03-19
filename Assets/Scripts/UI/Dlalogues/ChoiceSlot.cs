using System.Threading.Tasks;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class ChoiceSlot : SerializedMonoBehaviour
    {
        public IDialogueReplica Next => _nextReplica;

        
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private Image inputActionIcon;

        [OdinSerialize] private IUIStateAnimationAppearing appearingAnimation;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;
        [OdinSerialize] private IUIStateAnimationAppearing selectAnimation;
        
        private IDialogueReplica _nextReplica;

        public void SetText(string title) => titleTMP.text = title;

        public void SetBindIcon(Sprite icon)
        {
            inputActionIcon.sprite = icon;
        }
        public void Activate(ReplicaNode next)
        {
            _nextReplica = next;

            appearingAnimation.PlayAnimation();
        }
        public async Task Select()
        {
            await selectAnimation.PlayAnimation();
        }
        public void Reset()
        {
            _nextReplica = null;

            disappearingAnimation.PlayAnimation();
        }
    }
}