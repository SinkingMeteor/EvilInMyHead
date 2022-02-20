using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace SheldierEditor.DialogueSystem
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

            tree.AddAssetAtPath("Dialogues Localization", "Assets/Editor/Graphs/ScriptableObjects/LocalizationWindow.asset");
            return tree;
        }

        private void OnDisable()
        {
            SaveChanges();
        }
    }
}