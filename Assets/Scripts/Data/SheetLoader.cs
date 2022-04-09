using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace Sheldier.Data
{
    [ExecuteInEditMode]
    public abstract class SheetLoader : SerializedMonoBehaviour
    {
        [Serializable]
        private struct Sheet
        {
            public string Gid;
            public string TableID;
        }
        [Serializable] private class RSheetDict : Dictionary<string, Sheet> { }
        
        [OdinSerialize] private RSheetDict _sheets = new RSheetDict();

        private readonly List<string> _loading = new List<string> ();
        private const string _urlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";
        private const string _pathConfigs = "/Resources/JsonData/Data/";
        private const string _extension = ".json";
        
        /// <summary>
        /// Async return string CSV data
        /// </summary>
        protected void Load(string sheetName, Action<string> onLoaded)
        {
            if (_loading.Contains(sheetName))
            {
                Debug.LogWarning($"{this} : Already Loading : {sheetName}");
                return;
            }
            
            if (!_sheets.ContainsKey(sheetName))
            {
                Debug.LogError($"{this} : Sheet id does not exist : {sheetName} ");
                return;
            }
            
            _loading.Add(sheetName);
            StartCoroutine(LoadCoroutine(sheetName, onLoaded));
        }

        protected void LoadAll(string[] sheetNames, Action<string[]> onLoaded)
        {
            foreach (var sheetName in sheetNames)
            {
                _loading.Add(sheetName);
            }

            StartCoroutine(LoadAllCoroutine(sheetNames, onLoaded));
        }

        private IEnumerator LoadAllCoroutine(string[] sheetNames, Action<string[]> onLoaded)
        {
            string[] results = new string[sheetNames.Length];
            
            for (int i = 0; i < sheetNames.Length; i++)
            {
                var sheet = _sheets[sheetNames[i]];
                var url = string.Format(_urlPattern, sheet.TableID, sheet.Gid);
                Debug.Log($"Downloading: <color=yellow>{sheetNames[i]}</color> ...");
                var request = UnityWebRequest.Get(url);
                if (!request.isDone)
                {
                    yield return request.SendWebRequest();
                }
                if (request.error == null)
                {

                    Debug.Log($"Downloaded : <color=green>{sheetNames[i]}</color>");
                    _loading.Remove(sheetNames[i]);
                    results[i] = request.downloadHandler.text;
                }
            }
            onLoaded?.Invoke(results);
        }

        private IEnumerator LoadCoroutine(string sheetName, Action<string> onLoaded)
        {
            var sheet = _sheets[sheetName];
            var url = string.Format(_urlPattern, sheet.TableID, sheet.Gid);
            Debug.Log($"Downloading: <color=yellow>{sheetName}</color> ...");

            var request = UnityWebRequest.Get(url);
            if (!request.isDone)
            {
                yield return request.SendWebRequest();
            }

            if (request.error == null)
            {

                Debug.Log($"Downloaded : <color=green>{sheetName}</color>");
                _loading.Remove(sheetName);
                onLoaded?.Invoke(request.downloadHandler.text);
                yield break; 
            }
                
            _loading.Remove(sheetName);
            Debug.LogError($"{this} : {request.error}");
        }
        public static void Save<T>(T[] data, string name, bool prettyPrint = false)
        {
            if (data == null || data.Length == 0)
            {
                Debug.LogError($"Data is missing or empty");
            }
            
            var path = Application.dataPath + _pathConfigs + name + _extension;
            var jsonData = JsonHelper.ToJson(data, prettyPrint);
            File.WriteAllText(path, jsonData);
        }
    }
}