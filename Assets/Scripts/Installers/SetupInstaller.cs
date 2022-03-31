using Sheldier.Setup;
using Zenject;

namespace Sheldier.Installers
{
    public class SetupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoadingOperation>().AsSingle();
            Container.Bind<UILoadingOperation>().AsSingle();
            Container.Bind<SceneSetupOperation>().AsSingle();
        }
    }
}