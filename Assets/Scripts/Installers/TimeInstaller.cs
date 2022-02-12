using Sheldier.Common;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class TimeInstaller : MonoInstaller
    {
        [SerializeField] private TickHandler tickHandler;
        [SerializeField] private LateTickHandler lateTickHandler;
        [SerializeField] private FixedTickHandler fixedTickHandler;
        public override void InstallBindings()
        {
            Container.Bind<TickHandler>().FromInstance(tickHandler).AsSingle();
            Container.Bind<LateTickHandler>().FromInstance(lateTickHandler).AsSingle();
            Container.Bind<FixedTickHandler>().FromInstance(fixedTickHandler).AsSingle();
        }
    }
}