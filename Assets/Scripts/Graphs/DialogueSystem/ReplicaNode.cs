using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [NodeWidth(300), System.Serializable]
    public class ReplicaNode : Node, IDialogueReplica
    {
        [Input] public ReplicaNode input;

        public string Replica => _replica;
        public ConversationPerson Person => _person;
        public IReadOnlyList<ReplicaChoice> Choices => _choices;

        public int Index => _index;
        
        [TableColumnWidth(300)]
        [SerializeField, Multiline] private string _replica;
        [TableColumnWidth(25)]
        [SerializeField, ReadOnly] private int _index;
        [TableColumnWidth(500)]
        [SerializeField] private List<ReplicaChoice> _choices;

        [SerializeField] private ConversationPerson _person;

        public override object GetValue(NodePort port)
        {
            return this;
        }

        public void Clear()
        {
            _choices.Clear();
            _replica = String.Empty;
            var outputs = DynamicOutputs.ToArray();
            for (int i = 0; i < outputs.Length; i++)
            {
                RemoveDynamicPort(outputs[i]);
            }
        }

#if UNITY_EDITOR
        public void SetIndex(int index) => _index = index;
        public void SetReplica(string localizationKey) => _replica = localizationKey;
        public void CreateChoice()
        {
            var port = AddDynamicOutput(typeof(ReplicaNode), ConnectionType.Override, TypeConstraint.None, "Choice: " + Ports.Count());

            if (_choices == null)
                _choices = new List<ReplicaChoice>();
            _choices.Add(new ReplicaChoice(port));
        }

        public void RemoveLastPort()
        {
            RemoveDynamicPort(DynamicPorts.Last());
            _choices.RemoveAt(_choices.Count - 1);
        }
#endif

    }
    public interface IDialogueReplica
    {
        public string Replica { get; }
        public ConversationPerson Person { get; }
        public IReadOnlyList<ReplicaChoice> Choices { get; }
    }
}