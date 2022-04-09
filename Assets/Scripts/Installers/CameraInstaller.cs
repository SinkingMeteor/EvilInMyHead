using Sheldier.Common;
using Zenject;

namespace Sheldier.Installers
{
    public class CameraInstaller : Installer<CameraInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CameraHandler>().AsSingle();
        }
    }
}