using Sheldier.Actors.Pathfinding;
using Zenject;

namespace Sheldier.Installers
{
    public class PathFindingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PathSeeker>().AsSingle();
            Container.Bind<PathProvider>().AsSingle();
        }
    }
}