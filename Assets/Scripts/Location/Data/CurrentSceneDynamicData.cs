using System;
using Newtonsoft.Json;
using Sheldier.Common.SaveSystem;

namespace Sheldier.GameLocation
{
    [Serializable]
    public class CurrentSceneDynamicData : ISavable
    {
        public string CurrentLocationID => _currentLocationID;
        
        private string _currentLocationID;
        public CurrentSceneDynamicData(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }

        public void SetCurrentScene(string locationID)
        {
            _currentLocationID = locationID;
        }

        public string GetSaveName()
        {
            return GetType().ToString();
        }

        public string Save() => JsonConvert.SerializeObject(_currentLocationID);

        public void Load(string JSONedText) => _currentLocationID = JsonConvert.DeserializeObject<string>(JSONedText);
    }
}