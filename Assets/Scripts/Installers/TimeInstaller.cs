using Sheldier.Common;
using Sheldier.Common.Asyncs;
using Sheldier.Common.Pause;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class TimeInstaller : MonoInstaller<TimeInstaller>
    {
        [SerializeField] private TickHandler tickHandler;
        [SerializeField] private LateTickHandler lateTickHandler;
        [SerializeField] private FixedTickHandler fixedTickHandler;
        private PauseNotifier _pauseNotifier;

        public override void InstallBindings()
        {
            _pauseNotifier = new PauseNotifier();
            
            Container.Bind<PauseNotifier>().FromInstance(_pauseNotifier).AsSingle();
            
            tickHandler.SetDependencies(_pauseNotifier);
            lateTickHandler.SetDependencies(_pauseNotifier);
            fixedTickHandler.SetDependencies(_pauseNotifier);
            
            Container.Bind<TickHandler>().FromInstance(tickHandler).AsSingle();
            Container.Bind<LateTickHandler>().FromInstance(lateTickHandler).AsSingle();
            Container.Bind<FixedTickHandler>().FromInstance(fixedTickHandler).AsSingle();
            Container.Bind<AsyncWaitersFactory>().AsSingle().NonLazy();
        }
    }
}