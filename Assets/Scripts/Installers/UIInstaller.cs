using Sheldier.UI;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UIStatesController>().AsSingle();
            Container.Bind<PersistUI>().AsSingle();
            Container.Bind<UIInstaller>().FromInstance(this).AsSingle();
            Container.Bind<InteractHintController>().AsSingle();
        }

        public void InjectUIState(GameObject obj)
        {
            Container.InjectGameObject(obj);
        }
    }
}