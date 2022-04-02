using Sheldier.GameLocation;
using Zenject;

namespace Sheldier.Installers
{
    public class LocationInstaller : Installer<LocationInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLocationController>().AsSingle();
        }
    }
}