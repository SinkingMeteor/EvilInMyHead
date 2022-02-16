using UnityEngine;

namespace Sheldier.Common.Audio
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        public abstract void Play(AudioUnit audioUnit);
    }
}