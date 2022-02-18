using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueGraph", menuName = "Sheldier/Graph/DialogueGraph")]
    public class DialogueSystemGraph : NodeGraph
    {
        [SerializeField] private ReplicaNode _initialReplica;
        [SerializeField] private string _dialogueLocalizationKey;

        public string LocalizationKey => _dialogueLocalizationKey;
        public ReplicaNode StartDialogue() => _initialReplica;


        public void SetLocalizationKey(string key)
        {
            _dialogueLocalizationKey = key;
        }
    }
    
}
