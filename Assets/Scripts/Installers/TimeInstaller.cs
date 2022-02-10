using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class TimeInstaller : MonoInstaller
    {
        [SerializeField] private TickHandler tickHandler;
        public override void InstallBindings()
        {
            Container.Bind<TickHandler>().FromInstance(tickHandler).AsSingle();
        }
    }
}