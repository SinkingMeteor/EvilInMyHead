using System;
using System.Threading.Tasks;
using Sheldier.Common.Asyncs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sheldier.Common
{
    public class ResourceLoader
    {
        public T Load<T>(string filepath) where T : Object
        {
            var data = Resources.Load<T>(filepath);
            if(data == null)
                throw new NullReferenceException($"Asset of type {typeof(T)} from {filepath} can't be loaded");

            return data;
        }
        
        public T[] LoadAll<T>(string path) where T : Object
        {
            var data = Resources.LoadAll<T>(path);
            if(data == null)
                throw new NullReferenceException($"Assets of type {typeof(T)} from {path} can't be loaded");

            return data;
        }

        public async Task<T> LoadAsync<T>(string filepath) where T : Object
        {
            var asyncHandle = Resources.LoadAsync<T>(filepath);
            await AsyncWaitersFactory.WaitUntil(() => asyncHandle.isDone);
            if(asyncHandle.asset == null)
                throw new NullReferenceException($"Asset of type {typeof(T)} from {filepath} can't be loaded");
            if(asyncHandle.asset is not T castedAsset)
                throw new InvalidCastException($"Asset of type {typeof(T)} from {filepath} can't be casted");
            return castedAsset;
        }
    }
}