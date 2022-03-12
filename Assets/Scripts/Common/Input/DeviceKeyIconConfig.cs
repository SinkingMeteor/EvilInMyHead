using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Common
{
    [CreateAssetMenu(menuName = "Sheldier/Common/Input/DeviceKeyIconConfig", fileName = "DeviceKeyIconConfig")]
    public class DeviceKeyIconConfig : BaseScriptableObject
    {
        public IReadOnlyDictionary<string, KeyIconData> KeyIconCollection => keyIconCollection;
        [OdinSerialize] private Dictionary<string, KeyIconData> keyIconCollection;

        [System.Serializable]
        public struct KeyIconData
        {
            [PreviewField] public Sprite KeyIcon;
        }
    }
    
    
}