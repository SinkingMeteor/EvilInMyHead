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
        private readonly ScenePlayerController _scenePlayerController;
        private readonly DialoguesProvider dialoguesProvider;
        private readonly ICameraFollower _cameraFollower;
        private readonly GameObject _interactBase;
        private Material _interactMaterial;

        public ActorInteractBuilder(ScenePlayerController scenePlayerController, DialoguesProvider dialoguesProvider, ICameraFollower cameraFollower)
        {
            _cameraFollower = cameraFollower;
            _scenePlayerController = scenePlayerController;
            this.dialoguesProvider = dialoguesProvider;
            _interactBase = Resources.Load<GameObject>(ResourcePaths.ACTOR_INTERACT_MODULE);
            _interactMaterial = Resources.Load<Material>(ResourcePaths.UNLIT_OUTLINE_MATERIAL);
        }
        public void Build(Actor actor, ActorStaticBuildData buildData)
        {
            if (!buildData.CanInteract && buildData.InteractType == GameplayConstants.INTERACT_TYPE_NONE) return;
                
            GameObject body = GameObject.Instantiate(_interactBase, actor.transform, true);

            if (buildData.CanInteract)
            {
                var interactNotifier = body.AddComponent<ActorsInteractNotifier>();
                actor.AddExtraModule(interactNotifier);
            }

            if (buildData.InteractType == GameplayConstants.INTERACT_TYPE_NONE) return;

            actor.AddExtraModule(CreateInteractReceiver(body, buildData.InteractType));
        }

        private IExtraActorModule CreateInteractReceiver(GameObject body, string interactType)
        {
            return interactType switch
            {
                GameplayConstants.INTERACT_TYPE_REPLACE => CreateReplaceReceiver(body),
                GameplayConstants.INTERACT_TYPE_TALK => CreateTalkReceiver(body),
                GameplayConstants.INTERACT_TYPE_NONE => throw new ArgumentOutOfRangeException(nameof(interactType), interactType, null),
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
            receiver.SetDependencies(_scenePlayerController, _interactMaterial, _cameraFollower);
            return receiver;
        }
            
    }
}