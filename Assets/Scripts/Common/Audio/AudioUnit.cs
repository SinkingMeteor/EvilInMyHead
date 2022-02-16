using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Common.Audio
{
    [CreateAssetMenu(fileName = "AudioUnit", menuName = "Sheldier/Common/AudioUnit")]
    public class AudioUnit : BaseScriptableObject
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] [EnumToggleButtons] private AudioInterruptingType audioInterruptingType;
        
        public AudioClip Clip => audioClip;
        public AudioInterruptingType InterruptingType => audioInterruptingType;
    }
}