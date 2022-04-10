using System;

namespace Sheldier.Common
{
    public struct DialoguePlayRequest
    {
        public string DialogueId;
        public string[] ActorsGuidsInDialogue;
        public Action OnDialogueCompleted;
    }
}