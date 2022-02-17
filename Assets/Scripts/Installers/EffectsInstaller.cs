using Sheldier.Factories;
using Sheldier.Gameplay.Effects;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class EffectsInstaller : MonoInstaller
    {
        [SerializeField] private EffectsDataMap effectsDataMap;
        public override void InstallBindings()
        {
            Container.Bind<EffectsDataMap>().FromInstance(effectsDataMap).AsSingle();
            Container.Bind<ActorsEffectFactory>().AsSingle();
        }
    }
}