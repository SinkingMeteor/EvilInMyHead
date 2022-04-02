using Sheldier.Constants;
using Zenject;

namespace Sheldier.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputInstaller.InstallFromResource(ResourcePaths.INPUT_INSTALLER, Container);
            AudioInstaller.InstallFromResource(ResourcePaths.AUDIO_INSTALLER, Container);
            PoolInstaller.InstallFromResource(ResourcePaths.POOLS_INSTALLER, Container);
            TimeInstaller.InstallFromResource(ResourcePaths.TIME_INSTALLER, Container);
            LocalizationInstaller.Install(Container);
            AddressablesInstaller.Install(Container);
            PathFindingInstaller.Install(Container);
            DatabaseInstaller.Install(Container);
            GameplayInstaller.Install(Container);
            LocationInstaller.Install(Container);
            DialogueInstaller.Install(Container);
            EffectsInstaller.Install(Container);
            ActorsInstaller.Install(Container);
            CameraInstaller.Install(Container);
            SetupInstaller.Install(Container);
            ItemInstaller.Install(Container);
            UIInstaller.Install(Container);
        }
    }
}