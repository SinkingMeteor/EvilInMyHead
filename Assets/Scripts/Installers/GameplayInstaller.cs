using Sheldier.Actors.Inventory;
using Sheldier.Common.Cutscene;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Inventory>().AsSingle();
            Container.Bind<CutsceneController>().AsSingle();
            Container.Bind<GameplayInstaller>().AsSingle();
        }

        public void InjectGameObject(GameObject gameObject)
        {
            Container.InjectGameObject(gameObject);
        }
    }
}