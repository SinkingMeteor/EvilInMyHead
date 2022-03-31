using Sheldier.GameLocation;
using Zenject;

namespace Sheldier.Installers
{
    public class LocationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLocationController>().AsSingle();
        }
    }
}