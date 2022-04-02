using Sheldier.Actors;
using Sheldier.Common;
using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueGraph", menuName = "Sheldier/Graph/DialogueGraph")]
    public class DialogueSystemGraph : NodeGraph
    {
        public string LocalizationKey => _dialogueLocalizationKey;
        public ReplicaNode StartReplica => _initialReplica;
        public DataReference[] AdditionalPersons => additionalPersons;
        
        [SerializeField] private ReplicaNode _initialReplica;
        [SerializeField] private string _dialogueLocalizationKey;
        [SerializeField] private DataReference[] additionalPersons;
        public void SetLocalizationKey(string key)
        {
            _dialogueLocalizationKey = key;
        }
    }

    public enum ConversationPerson : byte
    {
        Initiator,
        Target,
        ThirdPerson,
        FourthPerson
    }
}
