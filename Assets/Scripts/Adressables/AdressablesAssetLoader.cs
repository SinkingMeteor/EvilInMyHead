using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sheldier.Setup
{
    public class AdressablesAssetLoader
    {
        private GameObject _cachedObject;
        protected async Task<T> LoadAssetAsync<T>(string assetID)
        {
            var handle = Addressables.InstantiateAsync(assetID);
            _cachedObject = await handle.Task;
            if (_cachedObject.TryGetComponent(out T component) == false)
                throw new NullReferenceException($"{typeof(T)} is undefined into loaded asset");
            return component;
        }

        protected void UnloadAsset()
        {
            if (_cachedObject == null)
                return;
            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}
