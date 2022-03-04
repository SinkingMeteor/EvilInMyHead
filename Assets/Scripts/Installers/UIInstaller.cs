using Sheldier.UI;
using Zenject;

namespace Sheldier.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIStatesController>().AsSingle();
        }
    }
}