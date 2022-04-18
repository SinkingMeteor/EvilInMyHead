using Sheldier.Common;
using Sheldier.Common.Utilities;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors.Interact
{
    public class TalkInteractReceiver : MonoBehaviour, IInteractReceiver, IExtraActorModule
    {
        public Transform Transform => transform;
        public Vector2 ColliderPosition => transform.position.DiscardZ() + _collider2D.offset;
        public float ColliderSize => _collider2D.radius;

        public string ReceiverType => GameplayConstants.INTERACT_RECEIVER_ACTOR;
        private Material _material;
        private Material _defaultMaterial;
        private ActorsView _actorsView;
        private DialoguesProvider _dialoguesProvider;
        private Actor _selfActor;
        private CircleCollider2D _collider2D;

        public void Initialize(ActorInternalData data)
        {
            _actorsView = data.Actor.ActorsView;
            _defaultMaterial = _actorsView.CurrentBodyMaterial;
            _selfActor = data.Actor;
            _collider2D = GetComponent<CircleCollider2D>();
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