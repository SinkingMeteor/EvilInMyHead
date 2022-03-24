using Sheldier.Common.Audio;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource source;

        public void PlayAudio(AudioUnit unit)
        {
            ChangePitch(unit);
            source.PlayOneShot(unit.Clip);
        }

        private void ChangePitch(AudioUnit unit)
        {
            var pitchOffset = UnityEngine.Random.Range(0.0f, unit.PitchVariative);
            source.pitch = 1 + pitchOffset;
        }
    }
}