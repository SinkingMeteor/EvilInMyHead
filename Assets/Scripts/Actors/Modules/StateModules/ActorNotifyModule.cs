using System;

namespace Sheldier.Actors
{
    public class ActorNotifyModule
    {
        public event Action OnActorAttacks;
        public event Action OnActorReloads;
        
        public void NotifyAttack() => OnActorAttacks?.Invoke();
        public void NotifyReloading() => OnActorReloads?.Invoke();
    }
}