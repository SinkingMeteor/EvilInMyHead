using Sheldier.Actors;
using Sheldier.Common;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Zenject;

namespace Sheldier.UI
{
    public class DialogueController : SerializedMonoBehaviour, IUIInitializable
    {
        private DialoguesProvider _dialoguesProvider;
        private UIStatesController _statesController;

        public void Initialize()
        {
            _dialoguesProvider.OnDialogueLoaded += StartDialogue;
        }

        [Inject]
        private void InjectDependencies(DialoguesProvider dialoguesProvider, UIStatesController statesController)
        {
            _statesController = statesController;
            _dialoguesProvider = dialoguesProvider;
        }
        private void StartDialogue(DialogueSystemGraph dialogue, Actor[] actors)
        {
            EnableState();
        }

        private void EnableState()
        {
            _statesController.Add(UIType.Dialogue);
        }

        private void DisableState()
        {
            _statesController.Remove(UIType.Dialogue);
        }
        public void Dispose()
        {
            _dialoguesProvider.OnDialogueLoaded -= StartDialogue;
        }
    }
}