using Sheldier.Common.Localization;
using Sheldier.Constants;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class LocalizationInstaller : Installer<LocalizationInstaller>
    {

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LocalizationProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<FontProvider>().AsSingle();
            Container.Bind<FontMap>().FromResource(ResourcePaths.FONT_MAP).AsSingle();
        }
    }
}