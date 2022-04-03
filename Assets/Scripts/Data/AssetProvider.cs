using System.Collections.Generic;
using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Data
{
    public abstract class AssetProvider<T> where T : Object
    {
        
        private Dictionary<string, T> _storageDictionary;
        public IReadOnlyDictionary<string, T> GetItems() => _storageDictionary;

        public bool IsItemExists(string ID) => _storageDictionary.ContainsKey(ID);

        public T Get(string ID, string path)
        {
            if (IsItemExists(ID))
                return _storageDictionary[ID];

            T asset = ResourceLoader.Load<T>(path + ID);
            _storageDictionary.Add(ID, asset);
            
            return asset;
        }

        public bool TryGet(string ID, string path, out T item)
        {
            if (_storageDictionary.TryGetValue(ID, out item))
                return true;

            item = Resources.Load<T>(path + ID);
            if (item == null)
                return false;
            return true;
        }

        public void Clear() => _storageDictionary.Clear();
    }
}