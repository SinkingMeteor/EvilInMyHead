using Sheldier.Common.Audio;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class AudioInstaller : MonoInstaller<AudioInstaller>
    {
        [SerializeField] private AudioMixerController audioMixerController;
        [SerializeField] private SoundPlayer soundPlayer;
        [SerializeField] private MusicPlayer musicPlayer;
        [SerializeField] private UIAudioPlayer uiAudioPlayer;
        public override void InstallBindings()
        {
            Container.Bind<ISoundPlayer>().FromInstance(soundPlayer);
            Container.Bind<IMusicPlayer>().FromInstance(musicPlayer);
            Container.Bind<IUIAudioPlayer>().FromInstance(uiAudioPlayer);
            Container.Bind<IAudioMixerController>().FromInstance(audioMixerController);
            Container.Bind<AudioMixerController>().FromInstance(audioMixerController);
        }
    }
}