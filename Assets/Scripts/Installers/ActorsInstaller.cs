using Sheldier.Actors;
using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class ActorsInstaller : MonoInstaller
    {
        [SerializeField] private ActorsMap actorsMap;
        public override void InstallBindings()
        {
            Container.Bind<ScenePlayerController>().AsSingle();
            Container.Bind<ActorSpawner>().AsSingle();
            Container.Bind<ActorsMap>().FromInstance(actorsMap).AsSingle();
            Container.Bind<ActorsInstaller>().FromInstance(this);
            
        }

        public void InjectActor(Actor actor)
        {
            Container.InjectGameObject(actor.gameObject);
        }
    }
}