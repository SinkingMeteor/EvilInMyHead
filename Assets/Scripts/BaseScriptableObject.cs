using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Sheldier.ScriptableObjects
{
    public class BaseScriptableObject : SerializedScriptableObject
    {
#if UNITY_EDITOR
        [Title("Editor Only", horizontalLine: true)]
        [Button]
        private void SaveChanges()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }

}
