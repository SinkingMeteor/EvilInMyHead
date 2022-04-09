using Sheldier.Common.SaveSystem;
using Zenject;

namespace Sheldier.Installers
{
    public class SaveInstaller : Installer<SaveInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SaveLoadDatabase>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveLoadHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveUtility>().AsSingle();
        }
    }
}