using System;
using Sheldier.Actors.Data;
using Sheldier.Actors.Interact;
using Sheldier.Common;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors.Builder
{
    public class ActorInteractBuilder : ISubBuilder
    {
        private readonly GameObject _interactBase;
        private readonly ScenePlayerController _scenePlayerController;
        private readonly ActorDataFactory _actorDataFactory;
        private readonly DialoguesProvider dialoguesProvider;
        private Material _interactMaterial;

        public ActorInteractBuilder(ScenePlayerController scenePlayerController, DialoguesProvider dialoguesProvider,
            ActorDataFactory actorDataFactory)
        {
            _actorDataFactory = actorDataFactory;
            _scenePlayerController = scenePlayerController;
            this.dialoguesProvider = dialoguesProvider;
            _interactBase = Resources.Load<GameObject>(ResourcePaths.ACTOR_INTERACT_MODULE);
            _interactMaterial = Resources.Load<Material>(ResourcePaths.UNLIT_OUTLINE_MATERIAL);
        }
        public void Build(Actor actor, ActorStaticBuildData buildData)
        {
            if (!buildData.CanInteract && buildData.InteractID == (int)InteractType.None) return;
                
            GameObject body = GameObject.Instantiate(_interactBase, actor.transform, true);

            if (buildData.CanInteract)
            {
                var interactNotifier = body.AddComponent<ActorsInteractNotifier>();
                actor.AddExtraModule(interactNotifier);
            }

            if (buildData.InteractID == (int)InteractType.None) return;

            actor.AddExtraModule(CreateInteractReceiver(body, buildData.InteractID));
        }

        private IExtraActorModule CreateInteractReceiver(GameObject body, int interactType)
        {
            return (InteractType)interactType switch
            {
                InteractType.Replace => CreateReplaceReceiver(body),
                InteractType.Talk => CreateTalkReceiver(body),
                InteractType.None => throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null),
                _ => throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null)
            };
        }

        private IExtraActorModule CreateTalkReceiver(GameObject body)
        {
            TalkInteractReceiver receiver = body.AddComponent<TalkInteractReceiver>();
            receiver.SetDependencies(_interactMaterial, dialoguesProvider);
            return receiver;
        }

        private IExtraActorModule CreateReplaceReceiver(GameObject body)
        {
            ReplaceInteractReceiver receiver = body.AddComponent<ReplaceInteractReceiver>();
            receiver.SetDependencies(_scenePlayerController, _interactMaterial);
            return receiver;
        }
            
    }
}