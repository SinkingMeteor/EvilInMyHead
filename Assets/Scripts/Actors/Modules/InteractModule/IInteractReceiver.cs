using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public interface IInteractReceiver
    {
        Transform Transform { get; }
        void OnEntered();
        void OnInteracted(ActorNotifyModule notifier);
        void OnExit();
        
    }
}