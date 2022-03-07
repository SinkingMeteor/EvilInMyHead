using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.UI
{
    public abstract class SelectableSlot : SerializedMonoBehaviour
    {
        [OdinSerialize] private IUIStateAnimationAppearing[] selectedAnimations;
        [OdinSerialize] private IUIStateAnimationDisappearing[] deselectedAnimations;

        protected void InitializeAnimations()
        {
            for (int i = 0; i < selectedAnimations.Length; i++)
                selectedAnimations[i].Initialize();
            for (int i = 0; i < deselectedAnimations.Length; i++)
                deselectedAnimations[i].Initialize();
        }

        public void Select()
        {
            for (int i = 0; i < selectedAnimations.Length; i++)
                selectedAnimations[i].PlayAnimation();
        }

        public void Deselect()
        {
            for (int i = 0; i < deselectedAnimations.Length; i++)
                deselectedAnimations[i].PlayAnimation();
        }
    }
}