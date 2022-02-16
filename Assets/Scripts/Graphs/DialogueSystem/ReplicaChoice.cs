using UnityEngine;
using XNode;

namespace Sheldier.Graphs.DialogueSystem
{
    [System.Serializable]
    public class ReplicaChoice
    {
        public string Choice => _choice;
        public ReplicaNode Next
        {
            get
            {
                var port = _nextNode.Connection;
                if (port == null)
                    return null;
                return port.node as ReplicaNode;
            }
        }

        [SerializeField] private string _choice;
        private NodePort _nextNode;

        public void SetChoiceLocalizationKey(string localizationKey)
        {
            _choice = localizationKey;
        }
        public ReplicaChoice(){}

        public ReplicaChoice(NodePort port)
        {
            _nextNode = port;
        }
    }
}