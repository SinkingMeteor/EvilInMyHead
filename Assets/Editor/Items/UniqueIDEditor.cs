using System;
using System.Collections.Generic;
using System.Linq;
using Sheldier.Item;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SheldierEditor.Item
{
    [CustomEditor(typeof(UniqueID))]
    public class UniqueIDEditor : Editor
    {
        [TableList] private List<IDConfig> ids = new List<IDConfig>();

        private void OnEnable()
        {
            var uniqueId = (UniqueID)target;
            if (string.IsNullOrEmpty(uniqueId.ID))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueID[] ids = FindObjectsOfType<UniqueID>();
                if(ids.Any(x => x != uniqueId && x.ID == uniqueId.ID))
                {
                    Generate(uniqueId);
                }
            }
        }

        private void Generate(UniqueID uniqueId)
        {
            uniqueId.SetID($"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}");
            if (Application.isPlaying)
                return;
            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);

        }
  
    }


}
