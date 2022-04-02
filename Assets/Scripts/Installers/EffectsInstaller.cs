using Sheldier.Factories;
using Zenject;

namespace Sheldier.Installers
{
    public class EffectsInstaller : Installer<EffectsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ActorsEffectFactory>().AsSingle();
        }
    }
}