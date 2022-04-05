using System.Collections.Generic;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Data
{
    public abstract class AssetProvider<T> where T : Object
    {
        
        protected abstract string Path { get; }
        
        private Dictionary<string, T> _storageDictionary;
        private AssetPathProvider _pathsProvider;
        public IReadOnlyDictionary<string, T> GetItems() => _storageDictionary;

        public bool IsItemExists(string ID) => _storageDictionary.ContainsKey(ID);

        public void Initialize()
        {
            _pathsProvider = ResourceLoader.Load<AssetPathProvider>(Path);
            _storageDictionary = new Dictionary<string, T>();
        }
        
        public T Get(string ID)
        {
            if (IsItemExists(ID))
                return _storageDictionary[ID];

            if (!_pathsProvider.PathsDatabase.TryGetValue(ID, out string assetPath))
            {
                Debug.LogError($"Path for {ID} can't be loaded");
                return null;
            }
            T asset = ResourceLoader.Load<T>(assetPath + ID);
            _storageDictionary.Add(ID, asset);
            
            return asset;
        }
        public void Clear() => _storageDictionary.Clear();
    }
}