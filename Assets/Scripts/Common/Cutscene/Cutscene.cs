using System;
using Sheldier.Actors;
using Sheldier.Actors.AI;
using Sheldier.Actors.Data;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common.Pause;
using Sheldier.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Common.Cutscene
{
    public class Cutscene : SerializedMonoBehaviour
    {
        [OdinSerialize] private ICutsceneElement[] cutsceneElements;
        
        private CutsceneInternalData _internalData;
        
        public void SetDependencies(SceneActorsDatabase sceneActorsDatabase, PauseNotifier pauseNotifier, PathProvider pathProvider,
            DialoguesProvider dialoguesProvider, Database<ActorDynamicConfigData> dynamicConfigDatabase, Actor controlledActor)
        {
            ActorsAIMoveModule moveModule = new ActorsAIMoveModule();
            moveModule.SetDependencies(pathProvider, pauseNotifier);
            _internalData = new CutsceneInternalData(sceneActorsDatabase, moveModule, controlledActor, dialoguesProvider, dynamicConfigDatabase);
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
