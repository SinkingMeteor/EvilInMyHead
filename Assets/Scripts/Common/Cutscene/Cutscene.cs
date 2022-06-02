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
        
        public void Play(Action<Cutscene> onCutsceneComplete)
        {
            CutsceneCoroutine(onCutsceneComplete);
        }

        private async void CutsceneCoroutine(Action<Cutscene> onCutSceneComplete)
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

            onCutSceneComplete?.Invoke(this);
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
