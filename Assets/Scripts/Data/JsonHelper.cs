using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Sheldier.Data
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<Wrapper<T>>(json).Items;
        }

        public static string ToJson<T>(T[] array)
        {
            return JsonConvert.SerializeObject(new Wrapper<T> {Items = array});
        }
        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            return JsonUtility.ToJson(new Wrapper<T> {Items = array}, prettyPrint);
        }

    }
    
    [Serializable]
    public class Wrapper<T>
    {
        public T[] Items;
    }
}