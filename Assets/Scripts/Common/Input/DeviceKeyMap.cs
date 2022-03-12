using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Common
{
    [CreateAssetMenu(menuName = "Sheldier/Common/Input/DeviceKeyMap", fileName = "DeviceKeyMap")]
    public class DeviceKeyMap : BaseScriptableObject
    {
        [OdinSerialize] private Dictionary<string, DeviceKeyIconConfig> map;
        [SerializeField] private Sprite nullSprite;
        private const string DEFAULT_RAW_DEVICE_PATH = "Keyboard:/Keyboard";
        public Sprite GetInputIcon(string rawDevicePath, string actionType)
        {
            if (!map.ContainsKey(rawDevicePath))
                rawDevicePath = DEFAULT_RAW_DEVICE_PATH;
            if (!map[rawDevicePath].KeyIconCollection.ContainsKey(actionType))
                return nullSprite;
            return map[rawDevicePath].KeyIconCollection[actionType].KeyIcon;
        }
    }
}