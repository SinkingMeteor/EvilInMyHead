using Sheldier.UI;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIStatesController>().AsSingle();
            Container.Bind<UIInstaller>().FromInstance(this).AsSingle();
        }

        public void InjectUIState(GameObject obj)
        {
            Container.InjectGameObject(obj);
        }
    }
}