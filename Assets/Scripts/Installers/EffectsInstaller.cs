using Sheldier.Constants;
using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using Zenject;

namespace Sheldier.Installers
{
    public class EffectsInstaller : Installer<EffectsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EffectsDataMap>().FromResource(ResourcePaths.EFFECTS_MAP).AsSingle();
            Container.Bind<ActorsEffectFactory>().AsSingle();
        }
    }
}