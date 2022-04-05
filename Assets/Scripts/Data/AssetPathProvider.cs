using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace Sheldier.Data
{
    [CreateAssetMenu(fileName = "PathProvider", menuName = "Sheldier/Data/PathProvider")]
    public class AssetPathProvider : BaseScriptableObject
    {
        public IReadOnlyDictionary<string, string> PathsDatabase => pathsDatabase;

        [OdinSerialize, ReadOnly] private Dictionary<string, string> pathsDatabase;
        
        private const string ASSET_PATTERN = @"/\w+.asset";
        private const string RESOURCES_NAME = "Resources/";
        
#if UNITY_EDITOR
        [Button]
        private void LoadFilesInSubdirectories()
        {
            pathsDatabase = new Dictionary<string, string>();
            string path = AssetDatabase.GetAssetPath(this.GetInstanceID());
            Regex regex = new Regex(ASSET_PATTERN);
            path = regex.Replace(path, "/");
            path = path.Remove(0, 6);
            Debug.Log(path);
            string[] directories = Directory.GetDirectories(Application.dataPath + path, "*", SearchOption.AllDirectories);
            
            foreach (var directory in directories)
            {
                var resourceStartIndex = path.IndexOf(RESOURCES_NAME, StringComparison.Ordinal);
                var newPath = path.Remove(0, resourceStartIndex + RESOURCES_NAME.Length);

                DirectoryInfo info = new DirectoryInfo(directory);
                FileInfo[] files = info.GetFiles("*");
                for (int i = 0; i < files.Length; i++)
                {
                    var filepathIndex = directory.IndexOf(newPath, StringComparison.Ordinal);
                    var filePath = directory.Remove(0, filepathIndex + newPath.Length);
                    if(files[i].Extension == ".meta")
                        continue;
                    var totalPath =  newPath + filePath + "/";
                    pathsDatabase.Add(Path.GetFileNameWithoutExtension(files[i].Name), totalPath);
                }
            }
        }
#endif
    }
}