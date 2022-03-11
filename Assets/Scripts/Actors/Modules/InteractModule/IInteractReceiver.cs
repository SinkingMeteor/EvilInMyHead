using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public interface IInteractReceiver
    {
        Transform Transform { get; }
        void OnEntered();
        bool OnInteracted(Actor actor);
        void OnExit();
        
    }
}