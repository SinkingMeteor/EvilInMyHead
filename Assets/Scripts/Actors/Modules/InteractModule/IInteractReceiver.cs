using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public interface IInteractReceiver
    {
        Transform Transform { get; }
        public string ReceiverType { get; }
        void OnEntered();
        bool OnInteracted(Actor actor);
        void OnExit();
        
    }
}