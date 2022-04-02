using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        [SerializeField] private InputProvider inputProvider;
        [SerializeField] private InputBindHandler bindHandler;
        public override void InstallBindings()
        {
            Container.Bind<IGameplayInputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<IInventoryInputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<IDialoguesInputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<InputProvider>().FromInstance(inputProvider).AsSingle();
            Container.Bind<IInputRebinder>().FromInstance(bindHandler).AsSingle();
            Container.Bind<InputBindHandler>().FromInstance(bindHandler).AsSingle();
            Container.Bind<IInputBindIconProvider>().FromInstance(bindHandler).AsSingle();
        }
    }
}
