using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public interface IInteractReceiver
    {
        Transform Transform { get; }
        void OnEntered();
        void OnInteracted(Actor actor);
        void OnExit();
        
    }
}