using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueGraph", menuName = "Sheldier/Graph/DialogueGraph")]
    public class DialogueSystemGraph : NodeGraph
    {
        [SerializeField] private ReplicaNode _initialReplica;

        public ReplicaNode StartDialogue() => _initialReplica;
    }
    
}
