using Sheldier.Setup;
using Zenject;

namespace Sheldier.Installers
{
    public class AdressablesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingScreenProvider>().AsSingle();
        }
    }
}
