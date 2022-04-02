using Sheldier.Common.Localization;
using Sheldier.Constants;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class LocalizationInstaller : Installer<LocalizationInstaller>
    {
        private LocalizationProvider _localizationProvider;
        private FontProvider _fontProvider;

        public override void InstallBindings()
        {

            
            _localizationProvider = new LocalizationProvider();
            _fontProvider = new FontProvider();
            
            Container.BindInterfacesAndSelfTo<LocalizationProvider>().FromInstance(_localizationProvider).AsSingle();
            Container.BindInterfacesAndSelfTo<FontProvider>().FromInstance(_fontProvider).AsSingle();
         //  Container.Bind<ILocalizationProvider>().FromInstance(_localizationProvider).AsSingle();
          //  Container.Bind<LocalizationProvider>().FromInstance(_localizationProvider).AsSingle();
          //  Container.Bind<FontProvider>().FromInstance(_fontProvider).AsSingle();
          //  Container.Bind<IFontProvider>().FromInstance(_fontProvider).AsSingle();
            Container.Bind<FontMap>().FromResource(ResourcePaths.FONT_MAP).AsSingle();
        }
    }
}