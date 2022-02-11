using Sheldier.Setup;
using Zenject;

namespace Sheldier.Installers
{
    public class SetupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameGlobalSettings>().AsSingle();
            Container.Bind<SceneLoadingOperation>().AsSingle();
        }
    }
}