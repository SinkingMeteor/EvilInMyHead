using System;
using Sheldier.Actors;
using Sheldier.Actors.AI;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common.Pause;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Common.Cutscene
{
    public class Cutscene : SerializedMonoBehaviour
    {
        [OdinSerialize] private ICutsceneElement[] cutsceneElements;
        
        private CutsceneInternalData _internalData;
        
        public void SetDependencies(ActorSpawner actorSpawner, PauseNotifier pauseNotifier, PathProvider pathProvider, ScenePlayerController scenePlayerController,
            DialoguesProvider dialoguesProvider)
        {
            ActorsAIMoveModule moveModule = new ActorsAIMoveModule();
            moveModule.SetDependencies(pathProvider, pauseNotifier);
            _internalData = new CutsceneInternalData(actorSpawner, moveModule, scenePlayerController.ControlledActor, dialoguesProvider);
        }

        public void Play(Action onCutsceneComplete)
        {

            foreach (var cutsceneElement in cutsceneElements)
                cutsceneElement.SetDependencies(_internalData);
            
            CutsceneCoroutine(onCutsceneComplete);
        }

        private async void CutsceneCoroutine(Action onCutSceneComplete)
        {
            foreach (var element in cutsceneElements)
            {
                if (element.WaitElement)
                    await element.PlayCutScene();
                else
#pragma warning disable CS4014
                    element.PlayCutScene();
#pragma warning restore CS4014
            }

            onCutSceneComplete?.Invoke();
        }

#if UNITY_EDITOR
        [Button]
        private void CollectElements()
        {
            cutsceneElements = GetComponentsInChildren<ICutsceneElement>();
        }
#endif
    }
}
