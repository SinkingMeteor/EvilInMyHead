using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Sheldier.Editor.DialogueSystem
{
    public class LocalizationEditorWindow : OdinMenuEditorWindow
    {

        [MenuItem("Sheldier/DialoguesLocalization")]
        public static void Open()
        {
            GetWindow<LocalizationEditorWindow>().Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.AddAssetAtPath("Localization", "Assets/Scripts/Graphs/DialogueSystem/Editor/ScriptableObjects/LocalizationWindow.asset");
            return tree;
        }

        private void OnDisable()
        {
            SaveChanges();
        }
    }
}