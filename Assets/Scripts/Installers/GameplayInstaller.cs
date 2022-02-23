using Sheldier.Actors.Inventory;
using Zenject;

namespace Sheldier.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().AsSingle();
        }
        
    }
}