using Zenject;

namespace Sheldier.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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
            SaveInstaller.Install(Container);
            ItemInstaller.Install(Container);
            UIInstaller.Install(Container);
        }
    }
}