using Sheldier.Common;
using Zenject;

namespace Sheldier.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CameraHandler>().AsSingle();
        }
    }
}