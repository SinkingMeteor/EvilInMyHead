using Sheldier.Actors;
using Sheldier.Actors.Builder;
using Sheldier.Common;
using Sheldier.Constants;
using Zenject;

namespace Sheldier.Installers
{
    public class ActorsInstaller : Installer<ActorsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScenePlayerController>().AsSingle();
            Container.Bind<ActorSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ActorBuilder>().AsSingle();
        }
    }
}