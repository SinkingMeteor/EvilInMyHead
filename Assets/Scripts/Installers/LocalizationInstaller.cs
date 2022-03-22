using Sheldier.Common.Localization;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class LocalizationInstaller : MonoInstaller
    {
        [SerializeField] private FontMap fontMap;
        
        private LocalizationProvider localizationProvider;
        private FontProvider _fontProvider;

        public override void InstallBindings()
        {
            localizationProvider = new LocalizationProvider();
            _fontProvider = new FontProvider();
            
            Container.Bind<ILocalizationProvider>().FromInstance(localizationProvider).AsSingle();
            Container.Bind<LocalizationProvider>().FromInstance(localizationProvider).AsSingle();
            Container.Bind<FontProvider>().FromInstance(_fontProvider).AsSingle();
            Container.Bind<IFontProvider>().FromInstance(_fontProvider).AsSingle();
            Container.Bind<FontMap>().FromInstance(fontMap).AsSingle();
        }
    }
}