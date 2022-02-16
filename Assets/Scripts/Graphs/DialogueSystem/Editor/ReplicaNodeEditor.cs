using System;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using XNodeEditor;

namespace Sheldier.Editor.DialogueSystem
{
    [CustomNodeEditor(typeof(ReplicaNode))]
    public class ReplicaNodeEditor : NodeEditor
    {

        public override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
            ReplicaNode node = target as ReplicaNode;
            DialogueSystemGraph graph = node.graph as DialogueSystemGraph;
            GUILayout.Space(10);
            GUILayout.Label(graph.nodes.IndexOf(node).ToString(), NodeEditorResources.styles.nodeHeader);
            node.SetIndex(graph.nodes.IndexOf(node));
            GUILayout.Space(10);

        }

        public override void OnBodyGUI()
        {
            ReplicaNode node = target as ReplicaNode;
            base.OnBodyGUI();
            GUILayout.Space(10);
             if (GUILayout.Button("Create Choice")) node.CreateChoice();
             GUILayout.Space(10);
             if (GUILayout.Button("Remove Last Choice")) node.RemoveLastPort();
             GUILayout.Space(10);
             if (GUILayout.Button("Clear")) node.Clear();

        }
        
    }
}