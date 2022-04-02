using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Data;
using Sheldier.Setup;
using Zenject;

namespace Sheldier.Installers
{
    public class DatabaseInstaller : Installer<DatabaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StaticDataLoader>().AsSingle();
            Container.Bind<ActorDataFactory>().AsSingle();
            
            Container.Bind<Database<ActorStaticConfigData>>().To<ActorStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticBuildData>>().To<ActorStaticBuildDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticMovementData>>().To<ActorStaticMovementDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticDialogueData>>().To<ActorStaticDialogueDatabase>().AsSingle();
            
            Container.Bind<Database<ActorDynamicConfigData>>().To<ActorDynamicConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicMovementData>>().To<ActorDynamicMovementDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicDialogueData>>().To<ActorDynamicDialogueDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicEffectData>>().To<ActorDynamicEffectDatabase>().AsSingle();
        }
    }
}