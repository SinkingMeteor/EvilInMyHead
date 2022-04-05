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

        [Tooltip("Table sheet contains sheet id and tableId." +
                 "Table id on Google Spreadsheet. Let's say your table has the following url " +
                 "https://docs.google.com/spreadsheets/d/1RvKY3VE_y5FPhEECCa5dv4F7REJ7rBtGzQg9Z_B_DE4/edit#gid=331980525So " +
                 "your table id will be '1RvKY3VE_y5FPhEECCa5dv4F7REJ7rBtGzQg9Z_B_DE4' and sheet id will be '331980525' (gid parameter)")]
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