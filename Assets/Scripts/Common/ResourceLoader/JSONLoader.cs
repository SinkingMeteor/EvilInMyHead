using System;
using Newtonsoft.Json;

namespace Sheldier.Common
{
    public class JSONLoader
    {
        public static T[] Load<T>(string path)
        {
            Array<T> deserializedItems = default;
            try
            {
                deserializedItems = JsonConvert.DeserializeObject<Array<T>>(path);

            }
            catch(JsonReaderException)
            {
                
            }
            return deserializedItems.Items;
        }
    }
    [Serializable]
    public class Array<T>
    {
        public T[] Items;
    }
}