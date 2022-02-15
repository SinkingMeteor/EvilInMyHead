using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [NodeWidth(300), System.Serializable]
    public class ReplicaNode : Node
    {
        [Input] public ReplicaNode input;

        public string Replica => _replica;
        public IReadOnlyList<ReplicaChoice> Choices => _choices;
        
        [SerializeField, Multiline] private string _replica;
        private List<ReplicaChoice> _choices;

        public int OutputCount => DynamicOutputs.Count();
        
        public override object GetValue(NodePort port)
        {
            return this;
        }
        
        #if UNITY_EDITOR
        
        public void FillText(string replica, params string[] choices)
        {
            _replica = replica;
            
            List<string> choiceList = choices.ToList();
            List<NodePort> outputs = DynamicOutputs.ToList();
            
            if(choiceList.Count != outputs.Count)
                Debug.LogError($"Count of outputs and choices of replica {_replica} is different");
            
            _choices = new List<ReplicaChoice>();
            for (int i = 0; i < choiceList.Count; i++)
            {
                _choices.Add(new ReplicaChoice(outputs[i], choiceList[i]));
            }
        }
        public void CreateChoice()
        {
            AddDynamicOutput(typeof(ReplicaNode), ConnectionType.Multiple, TypeConstraint.None, "Choice: " + Ports.Count());
        }

        public void RemoveLastPort()
        {
            RemoveDynamicPort(DynamicPorts.Last());
        }
        #endif



    }
[System.Serializable]
    public class ReplicaChoice
    {
        public ReplicaNode NextState => _nextState;
        public string Choice => _choice;

        private string _choice;
        private ReplicaNode _nextState;
        
        public ReplicaChoice(NodePort output, string choice)
        {
            _choice = choice;
            _nextState = output.node as ReplicaNode;

        }
    }



}