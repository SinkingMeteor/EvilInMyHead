using Sheldier.Constants;
using Sheldier.Factories;
using Sheldier.Item;
using Zenject;

namespace Sheldier.Installers
{
    public class ItemInstaller : Installer<ItemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ItemMap>().FromResource(ResourcePaths.ITEMS_MAP).AsSingle();
            Container.Bind<ItemSpawner>().AsSingle();
            Container.Bind<ItemFactory>().AsSingle();
        }
    }
}