using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InputProvider inputProvider;
        public override void InstallBindings()
        {
            Container.Bind<IInputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<InputProvider>().FromInstance(inputProvider).AsSingle();
        }
    }

}
