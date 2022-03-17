using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public class TalkInteractReceiver : MonoBehaviour, IInteractReceiver, IExtraActorModule
    {
        public Transform Transform => transform;
        private Material _material;
        private Material _defaultMaterial;
        private ActorsView _actorsView;
        private DialoguesProvider dialoguesProvider;
        private Actor _selfActor;

        public void Initialize(ActorInternalData data)
        {
            _actorsView = data.Actor.ActorsView;
            _defaultMaterial = _actorsView.CurrentBodyMaterial;
            _selfActor = data.Actor;
        }

        public void SetDependencies(Material material, DialoguesProvider dialoguesProvider)
        {
            this.dialoguesProvider = dialoguesProvider;
            _material = material;
        }
        public void OnEntered()
        {
            _actorsView.SetMaterial(_material);
        }

        public bool OnInteracted(Actor interactingActor)
        {
            Debug.Log("Start talking");
            dialoguesProvider.FindDialogue(interactingActor, _selfActor);
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