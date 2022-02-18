using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.Item;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace SheldierEditor.Item
{
    public class IDConfigEditorWindow : OdinEditorWindow
    {
        [TableList] private List<IDConfig> ids = new List<IDConfig>();
        
        [MenuItem("Sheldier/Items")]
        public static void Open()
        {
            GetWindow<IDConfigEditorWindow>().Show();
        }
        
        
       /* public void OnInspectorGUI()
        {
            if (string.IsNullOrEmpty(uniqueId.ID))
            {
                Generate(uniqueId);
            }
            else
            {
                var guids = AssetDatabase.FindAssets("t:IDConfig",new string[]{"Assets/ScriptableObjects/Item"});
                List<object> assets = new List<object>();
                for( int i = 0; i < guids.Length; i++ )
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
                    var asset = AssetDatabase.LoadAssetAtPath<IDConfig>( assetPath );
                    if( asset != null )
                    {
                        assets.Add(asset);
                    }
                }
                List<IDConfig> ids = new List<IDConfig>();
                foreach (var obj in assets)      
                {
                    if (obj is IDConfig)
                    {
                        ids.Add((IDConfig)obj);
                    }
                }
                if(ids.Any(x => x != uniqueId && x.ID == uniqueId.ID))
                {
                    Generate(uniqueId);
                }
            }
            
        }
        private void Generate(IDConfig uniqueId)
        {
            uniqueId.SetID($"{uniqueId.name}_{Guid.NewGuid().ToString()}");
            EditorUtility.SetDirty(uniqueId);
        }*/
    }


}
