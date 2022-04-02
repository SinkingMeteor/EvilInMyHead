using Sheldier.Actors.Pathfinding;
using Zenject;

namespace Sheldier.Installers
{
    public class PathFindingInstaller : Installer<PathFindingInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Pathfinder>().AsSingle();
            Container.Bind<PathProvider>().AsSingle();
        }
    }
}