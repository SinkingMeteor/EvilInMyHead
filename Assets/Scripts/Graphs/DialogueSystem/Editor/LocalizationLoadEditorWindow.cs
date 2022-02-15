using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sheldier.Common.Localization;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Sheldier.Editor.DialogueSystem
{
    [CreateAssetMenu(fileName = "Localization", menuName = "Sheldier/Editor/LocalizationWindow")]
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
                    cellData.Toggle = false;
                else
                    cellData.Toggle = true;
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
        [TableList] public List<DialogueSystemGraph> DialoguesGraph;
        
        [HorizontalGroup("Dialogues/Load")]
        [Button(ButtonSizes.Large)]
        public void LoadGraphs()
        {
            DialoguesGraph = new List<DialogueSystemGraph>();
            object[] graphs = AssetDatabase.LoadAllAssetsAtPath(GraphsPath);
            foreach (var graph in graphs)
            {
                DialogueSystemGraph convertedGraph = graph as DialogueSystemGraph;
                if(convertedGraph == null)
                    continue;
                DialoguesGraph.Add(convertedGraph);
            }
        }
        
        
        public class LocalizationCellData
        {
            [TableColumnWidth(50)]
            public bool Toggle;
            [TableColumnWidth(160)]
            public string Key;
            [TableColumnWidth(160)]
            public string Value;
        }
        
        public enum FilterSettings
        {
            UI,
            Dialogues
        }

    }
}