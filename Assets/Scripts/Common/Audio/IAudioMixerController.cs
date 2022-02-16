namespace Sheldier.Common.Audio
{
    public interface IAudioMixerController
    {
        void SetVolume(AudioTrackType trackType, float volume);
        float GetVolume(AudioTrackType trackType);
    }
}