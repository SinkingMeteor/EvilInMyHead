using System;
using UnityEngine;

namespace Sheldier.Common.SaveSystem
{
    public class SaveUtility : ISaveUtility
    {
        public bool IsLoadFileExists(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && PlayerPrefs.HasKey(fileName);
        }

        public void SaveData(string data, string key)
        {
            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
        }

        public string GetData(string key)
        {
            if (IsLoadFileExists(key))
                return PlayerPrefs.GetString(key);
            return String.Empty;
        }
    }
}