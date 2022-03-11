using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private InputBindHandler bindHandler;
        public override void InstallBindings()
        {
            Container.Bind<IInputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<IUIInputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<InputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<IInputRebinder>().FromInstance(bindHandler).AsSingle();
            Container.Bind<IInputBindIconProvider>().FromInstance(bindHandler).AsSingle();
        }
    }
}
