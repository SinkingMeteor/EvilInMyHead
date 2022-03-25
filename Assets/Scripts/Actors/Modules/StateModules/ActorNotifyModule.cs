using System;

namespace Sheldier.Actors
{
    public class ActorNotifyModule
    {
        public event Action OnActorAttacks;
        public event Action OnActorReloads;
        public event Action<ActorDirectionView> OnActorFalls;
        
        public void NotifyAttack() => OnActorAttacks?.Invoke();
        public void NotifyReloading() => OnActorReloads?.Invoke();
        public void NotifyFalling(ActorDirectionView view) => OnActorFalls?.Invoke(view);
    }
}