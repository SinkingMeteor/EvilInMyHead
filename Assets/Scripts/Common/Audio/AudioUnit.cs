using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Common.Audio
{
    [CreateAssetMenu(fileName = "AudioUnit", menuName = "Sheldier/Common/AudioUnit")]
    public class AudioUnit : BaseScriptableObject
    {
        public AudioClip Clip => audioClip;
        public AudioInterruptingType InterruptingType => audioInterruptingType;
        public float PitchVariative => _pitchVariative;
        
        [SerializeField] private AudioClip audioClip;
        [SerializeField] [EnumToggleButtons] private AudioInterruptingType audioInterruptingType;
        [SerializeField] private float _pitchVariative;

    }
}