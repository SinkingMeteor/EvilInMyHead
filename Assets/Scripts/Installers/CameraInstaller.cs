using Sheldier.Common;
using Zenject;

namespace Sheldier.Installers
{
    public class CameraInstaller : Installer<CameraInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CameraHandler>().AsSingle();
        }
    }
}