using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Common
{
    [CreateAssetMenu(menuName = "Sheldier/Common/Input/DeviceKeyIconConfig", fileName = "DeviceKeyIconConfig")]
    public class DeviceKeyIconConfig : BaseScriptableObject
    {
        public IReadOnlyDictionary<string, Sprite> KeyIconCollection => keyIconCollection;
        [OdinSerialize] private Dictionary<string, Sprite> keyIconCollection;
    }
}