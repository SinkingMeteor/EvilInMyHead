using System;
using Newtonsoft.Json;
using Sheldier.Constants;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sheldier.Common
{
    public class JSONLoader
    {
        public static T[] Load<T>(string filename)
        {
            var data = ResourceLoader.Load<TextAsset>(ResourcePaths.JSON_PATH_DIRECTORY + filename);
            if(data == null)
                throw new NullReferenceException($"Asset of type {typeof(T)} can't be loaded");
            string rawText = data.text;
            string fullJson = "{\"Items\":" + rawText + "}";
            var deserializedItems = JsonConvert.DeserializeObject<Array<T>>(fullJson);
            return deserializedItems.Items;
        }
    }
    [Serializable]
    public class Array<T>
    {
        public T[] Items;
    }
}