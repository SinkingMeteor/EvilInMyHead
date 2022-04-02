using Sheldier.Actors.Data;
using Sheldier.Data;
using Zenject;

namespace Sheldier.Installers
{
    public class DatabaseInstaller : Installer<DatabaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Database<ActorStaticConfigData>>().To<ActorStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticBuildData>>().To<ActorStaticBuildDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticMovementData>>().To<ActorStaticMovementDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticDialogueData>>().To<ActorStaticDialogueDatabase>().AsSingle();
        }
    }
}