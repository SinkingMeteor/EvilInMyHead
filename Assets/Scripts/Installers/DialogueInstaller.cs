using Sheldier.Common;
using Zenject;

namespace Sheldier.Installers
{
    public class DialogueInstaller : Installer<DialogueInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<DialoguesProvider>().AsSingle();
        }
    }
}