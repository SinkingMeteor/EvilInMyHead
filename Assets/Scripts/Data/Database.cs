using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sheldier.Common.SaveSystem;

namespace Sheldier.Data
{
    public abstract class Database<T> : ISavable where T : IDatabaseItem
    {
        private Dictionary<string, T> _storageDictionary;

        public int Count => _storageDictionary.Count;

        protected Database()
        {
            _storageDictionary = new Dictionary<string, T>();
        }

        public IEnumerable<T> GetItems() => _storageDictionary.Values;

        public bool IsItemExists(string ID) => _storageDictionary.ContainsKey(ID);

        public T Get(string ID) => _storageDictionary[ID];

        public void Set(IEnumerable<T> enumerable) => _storageDictionary = enumerable.ToDictionary(x => x.ID);
        
        public bool TryGet(string ID, out T item) => _storageDictionary.TryGetValue(ID, out item);
        
        public void Add(T item) => _storageDictionary.Add(item.ID, item);

        public void Remove(string ID) => _storageDictionary.Remove(ID);
        
        public void Clear() => _storageDictionary.Clear();

        public string Save() => JsonConvert.SerializeObject(_storageDictionary);
        
        public virtual string GetSaveName() => String.Empty;

        public void Load(string JSONedText) => _storageDictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(JSONedText);
    }
}