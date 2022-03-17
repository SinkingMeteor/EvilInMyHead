using Sheldier.Common;
using Zenject;

namespace Sheldier.Installers
{
    public class DialogueInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DialoguesProvider>().AsSingle();
        }
    }
}