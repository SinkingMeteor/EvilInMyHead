using Sheldier.Common;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public class TalkInteractReceiver : MonoBehaviour, IInteractReceiver, IExtraActorModule
    {
        public Transform Transform => transform;
        public string ReceiverType => GameplayConstants.INTERACT_RECEIVER_ACTOR;
        private Material _material;
        private Material _defaultMaterial;
        private ActorsView _actorsView;
        private DialoguesProvider _dialoguesProvider;
        private Actor _selfActor;

        public void Initialize(ActorInternalData data)
        {
            _actorsView = data.Actor.ActorsView;
            _defaultMaterial = _actorsView.CurrentBodyMaterial;
            _selfActor = data.Actor;
        }

        public void SetDependencies(Material material, DialoguesProvider dialoguesProvider)
        {
            _dialoguesProvider = dialoguesProvider;
            _material = material;
        }
        public void OnEntered()
        {
            _actorsView.SetMaterial(_material);
        }

        public bool OnInteracted(Actor interactingActor)
        {
            _dialoguesProvider.FindDialogue(interactingActor, _selfActor);
            return false;
        }

        public void OnExit()
        {
            _actorsView.SetMaterial(_defaultMaterial);
        }

        public void Dispose()
        {
        }
    }
}