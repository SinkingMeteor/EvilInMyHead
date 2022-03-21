using Sheldier.Actors.Inventory;
using Sheldier.Common.Cutscene;
using Zenject;

namespace Sheldier.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().AsSingle();
            Container.Bind<CutsceneController>().AsSingle();
        }
        
    }
}