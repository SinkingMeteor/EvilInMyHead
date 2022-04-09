using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Sheldier.Common.Audio
{
    public class AudioMixerController : MonoBehaviour, IAudioMixerController
    {
        [SerializeField] private AudioMixer audioMixer;

        private Dictionary<AudioTrackType, AudioTrackExposedData> _tracks;

        private const float MINIMAL_DB_VOLUME = -80f;
        private const float MAXIMAL_DB_VOLUME = 20f;

        public void Initialize()
        {
            _tracks = new Dictionary<AudioTrackType, AudioTrackExposedData>
            {
                {AudioTrackType.Master, new AudioTrackExposedData("Master")},
                {AudioTrackType.Music, new AudioTrackExposedData("Music")},
                {AudioTrackType.Effects, new AudioTrackExposedData("Effects")},
                {AudioTrackType.UI, new AudioTrackExposedData("UI")},
            };
        }
        
        private float ConvertToDB(float volume01)
        {
            return Mathf.Lerp(MINIMAL_DB_VOLUME, MAXIMAL_DB_VOLUME, volume01);
        }

        private float ConvertTo01(AudioTrackType trackType)
        {
            audioMixer.GetFloat(_tracks[trackType].Volume, out float dbVolume);
            return Mathf.InverseLerp(MINIMAL_DB_VOLUME, MAXIMAL_DB_VOLUME, dbVolume);
        }

        public float GetVolume(AudioTrackType trackType) => ConvertTo01(trackType);
        public void SetVolume(AudioTrackType trackType, float volume) => audioMixer.SetFloat(_tracks[trackType].Volume, ConvertToDB(volume));
    
    
        private class AudioTrackExposedData
        {
            public readonly string Volume;

            public AudioTrackExposedData(string trackName)
            {
                var builder = new StringBuilder();
                Volume = builder.Append(trackName).Append("Volume").ToString();
                builder.Clear();
            }
        }
    }
}