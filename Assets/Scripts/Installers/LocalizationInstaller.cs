using Sheldier.Common.Localization;
using Zenject;

namespace Sheldier.Installers
{
    public class LocalizationInstaller : MonoInstaller
    {
        private LocalizationProvider provider;
        public override void InstallBindings()
        {
            provider = new LocalizationProvider();
            Container.Bind<ILocalizationProvider>().FromInstance(provider).AsSingle();
            Container.Bind<LocalizationProvider>().FromInstance(provider).AsSingle();
        }
    }
}