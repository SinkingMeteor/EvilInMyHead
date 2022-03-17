using Sheldier.Actors;
using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueGraph", menuName = "Sheldier/Graph/DialogueGraph")]
    public class DialogueSystemGraph : NodeGraph
    {
        public string LocalizationKey => _dialogueLocalizationKey;
        public ReplicaNode StartDialogue() => _initialReplica;
        public ActorType[] AdditionalPersons => additionalPersons;
        
        [SerializeField] private ReplicaNode _initialReplica;
        [SerializeField] private string _dialogueLocalizationKey;
        [SerializeField] private ActorType[] additionalPersons;
        public void SetLocalizationKey(string key)
        {
            _dialogueLocalizationKey = key;
        }
    }

    public enum ConversationPerson
    {
        Initiator,
        Target,
        ThirdPerson,
        FourthPerson
    }
}
