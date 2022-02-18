using System.Collections.Generic;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace SheldierEditor.DialogueSystem
{
    
    [CreateAssetMenu(fileName = "LocalizationGraphWindow", menuName = "Sheldier/Editor/LocalizationGraphWindow")]
    public class DialogueGraphEditorWindow : OdinEditorWindow
    {
        private DialogueSystemGraph _graph;
        [TableList] public List<ReplicaCellData> NodesList;

        public void Open(DialogueSystemGraph dialogueSystemGraph)
        {
            NodesList = new List<ReplicaCellData>();
            _graph = dialogueSystemGraph;

            foreach (var node in _graph.nodes)
            {
                NodesList.Add(new ReplicaCellData((ReplicaNode)node));
            }
        }

        public class ReplicaCellData
        {
            private readonly ReplicaNode _replicaNode;

            [ShowInInspector] public int Index => _replicaNode.Index;
            [ShowInInspector] public string Replica => _replicaNode.Replica;
            [ShowInInspector] public IReadOnlyList<ReplicaChoice> Choices => _replicaNode.Choices;

            public ReplicaCellData(ReplicaNode replicaNode)
            {
                _replicaNode = replicaNode;
            }
        }
    }
}