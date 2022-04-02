using Sheldier.Setup;
using Zenject;

namespace Sheldier.Installers
{
    public class AddressablesInstaller : Installer<AddressablesInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingScreenProvider>().AsSingle();
        }
    }
}
