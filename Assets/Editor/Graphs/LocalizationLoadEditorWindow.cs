using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sheldier.Common.Localization;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SheldierEditor.DialogueSystem
{
    [CreateAssetMenu(fileName = "LocalizationWindow", menuName = "Sheldier/Editor/LocalizationWindow")]
    public class LocalizationLoadEditorWindow : OdinEditorWindow
    {
        private CsvLocalizationLoader _localizationLoader;
        
        [HideInInspector] public List<LocalizationCellData> LocalizationsPairs;
        [TitleGroup("Localization")][TableList] public List<LocalizationCellData> FilteredLocalizations;
        
        [HorizontalGroup("Localization/Load")]
        [Button(ButtonSizes.Large)]
        public void LoadKeys()
        {
            _localizationLoader = new CsvLocalizationLoader(new LocalizationPathProvider());
            LocalizationsPairs = new List<LocalizationCellData>();
            var _localizedText = _localizationLoader.LoadFile(Language.RU);
            foreach (var text in _localizedText)
            {
                LocalizationCellData cellData = new LocalizationCellData();
                cellData.Key = text.Key;
                cellData.Value = text.Value;
                if (string.IsNullOrEmpty(cellData.Key) || string.IsNullOrEmpty(cellData.Value))
                    cellData.IsValid = false;
                else
                    cellData.IsValid = true;
                LocalizationsPairs.Add(cellData);
            }
            FilterAll();
        }
        [TitleGroup("Localization")]

        [HorizontalGroup("Localization/Filter")]
        [Button]
        public void FilterAll() => FilterLocalization(null);
        
        [HorizontalGroup("Localization/Filter")]
        [Button]
        public void FilterByUI() => FilterLocalization("^ui.|[^a-z]ui.");
        
        [HorizontalGroup("Localization/Filter")]
        [Button]
        public void FilterByDialogues() => FilterLocalization("^dialogues.|[^a-z]dialogues.");

        [HorizontalGroup("Localization/Search")]
        [Button]
        public void SearchByFilter() => FilterLocalization($"^{SearchFilter}.|[^a-z]{SearchFilter}.");

        [HorizontalGroup("Localization/Search")] public string SearchFilter;
        
        private void FilterLocalization(string pattern)
        {
            if (LocalizationsPairs == null)
            {
                Debug.LogWarning("Load localization file first");
                return;
            }
            if (pattern == null)
            {
                FilteredLocalizations = LocalizationsPairs;
                return;
            }
            FilteredLocalizations = new List<LocalizationCellData>();
            Regex regex = new Regex(pattern);


            foreach (var pair in LocalizationsPairs)
            {
                if(regex.IsMatch(pair.Key))
                    FilteredLocalizations.Add(pair);
            }
        }
        [TitleGroup("Dialogues")]
        
        [Sirenix.OdinInspector.FilePath]  public string GraphsPath;
        [TableList] public List<GraphCellData> DialoguesGraph;
        
        [HorizontalGroup("Dialogues/Load")]
        [Button(ButtonSizes.Large)]
        public void LoadGraphs()
        {
            DialoguesGraph = new List<GraphCellData>();
            object[] graphs = AssetDatabase.LoadAllAssetsAtPath(GraphsPath);
            foreach (var graph in graphs)
            {
                DialogueSystemGraph convertedGraph = graph as DialogueSystemGraph;
                if(convertedGraph == null)
                    continue;
                DialoguesGraph.Add(new GraphCellData(convertedGraph, this));
            }
        }

   

        /*private void OnDisable()
        {
            if (DialoguesGraph == null)
                return;

            foreach (var graph in DialoguesGraph)
            {
                graph.InsertCallBack = null;
            }
        }*/
       
        public class GraphCellData
        {
            public DialogueSystemGraph Graph;
            [ShowInInspector] public string AssetName => Graph.name;
            [ShowInInspector] public string GraphLocalizationKey
            {
                get => Graph.LocalizationKey;
                set => Graph.SetLocalizationKey(value);
            }
            private readonly LocalizationLoadEditorWindow _loadEditorWindow;

            public GraphCellData(DialogueSystemGraph graph, LocalizationLoadEditorWindow loadEditorWindow)
            {
                _loadEditorWindow = loadEditorWindow;
                Graph = graph;
            }


            [VerticalGroup("EditGraph")]
            [Button("View")]
            private void EditGraph()
            {
                CreateWindow<DialogueGraphEditorWindow>().Open(Graph);
              // OpenGraph(Graph);
            }
        
            [VerticalGroup("EditGraph")]
            [Button("Clear")]
            private void ClearGraph()
            {
                foreach (var node in Graph.nodes)
                {
                    var replica = node as ReplicaNode;
                    replica.Clear();
                }
            }

            [VerticalGroup("EditGraph")]
            [Button("Insert")]
            private void InsertLocalizationKey()
            {
                InsertLocalizationKey(Graph, _loadEditorWindow.LocalizationsPairs);
            }

            private void InsertLocalizationKey(DialogueSystemGraph graph, List<LocalizationCellData> LocalizationsPairs)
        {
            List<string> foundKeys = new List<string>();
            
            if (LocalizationsPairs == null)
            {
                Debug.LogWarning("Load localization file first");
                return;
            }
            Regex regex = new Regex($"^{graph.LocalizationKey}.|[^a-z]{graph.LocalizationKey}.");

            foreach (var pair in LocalizationsPairs)
            {
                if (regex.IsMatch(pair.Key))
                {
                    foundKeys.Add(pair.Key);
                }
            }

            if (foundKeys.Count == 0)
            {
                Debug.Log($"Can't find any keys for graph {graph.name}");
                return;
            }


            for (int i = 1; i < foundKeys.Count + 1; i++)
            {
                Regex replicaRegex = new Regex("replica_" + i);
                var matches = foundKeys.Where(x => replicaRegex.IsMatch(x)).ToArray();
                
                if(matches.Length == 0)
                    continue;

                if (graph.nodes.Count < i)
                {
                    Debug.LogWarning($"Some nodes in graph {graph.name} aren't exist");
                    return;
                }
                
                var replicaNode = (ReplicaNode) graph.nodes[i - 1];
                replicaNode.SetReplica(matches[0]);

                
                Regex generalChoiceRegex = new Regex($"replica_{i}.choice_");
                var generalChoiceMatches = foundKeys.Where(x => generalChoiceRegex.IsMatch(x)).ToArray();
                
                if(generalChoiceMatches.Length == 0 || replicaNode.Choices == null || replicaNode.Choices.Count == 0)
                    continue;

                
                for (int j = 1; j < generalChoiceMatches.Length + 1; j++)
                {
                    Regex choiceRegex = new Regex("choice_" + j);
                    var choiceMatches = foundKeys.Where(x => choiceRegex.IsMatch(x)).ToArray();

                    if(choiceMatches.Length > 1)
                        Debug.LogWarning($"Found identical choices in replica {replicaNode.Replica}");

                    if (replicaNode.Choices.Count < j)
                    {
                        Debug.LogWarning($"Some choices in replica {replicaNode.Replica} aren't exist");
                        return;
                    }

                    replicaNode.Choices[j - 1].SetChoiceLocalizationKey(choiceMatches[0]);
                }

            }
            Debug.Log("Successfully inserted");
        }
        }
        public class LocalizationCellData
        {
            [TableColumnWidth(50)]
            public bool IsValid;
            [TableColumnWidth(160)]
            public string Key;
            [TableColumnWidth(160)]
            public string Value;
        }

}
}