using System.Linq;
using Sheldier.Data;
using UnityEngine;


namespace Sheldier.Common.SaveSystem
{
    public class SaveLoadHandler : ISaveLoadHandler
    {
        private readonly ISaveUtility _saveUtility;
        private readonly ISaveDatabase _saveDatabase;

        public SaveLoadHandler(ISaveUtility saveUtility, ISaveDatabase saveDatabase)
        {
            _saveUtility = saveUtility;
            _saveDatabase = saveDatabase;
        }
        public bool HasSaves(string saveDataKey)
        {
            return !string.IsNullOrEmpty(saveDataKey) && _saveUtility.IsLoadFileExists(saveDataKey);
        }
        
        public void SaveAll(string saveDataKey)
        {
            if (string.IsNullOrEmpty(saveDataKey))
            {
                Debug.LogError($"{this} : The key must not be Null or Empty");
                return;
            }

            var data = JsonHelper.ToJson(_saveDatabase
                .GetAll()
                .Select(x => new SaveData {Key = x.GetSaveName(), Data = x.Save()})
                .ToArray());
            _saveUtility.SaveData(data, saveDataKey);
        }
        
        public void LoadAll(string saveDataKey)
        {
            if (!HasSaves(saveDataKey))
            {
                Debug.LogError($"{this} : No save data exists for the key : {saveDataKey}");
                return;
            }

            var loadedData = _saveUtility.GetData(saveDataKey);
            var saveDatas = JsonHelper.FromJson<SaveData>(loadedData).ToDictionary(x => x.Key);

            foreach (var savable in _saveDatabase.GetAll())
            {
                var typeKey = savable.GetSaveName();
                if (!saveDatas.ContainsKey(typeKey))
                {
                    Debug.LogError($"{this} : FarmData does not exist Savable : {typeKey}");
                    continue;
                }

                savable.Load(saveDatas[typeKey].Data);
            }
        }
    }

    public struct SaveData
    {
        public string Key;
        public string Data;
    }
    
}

