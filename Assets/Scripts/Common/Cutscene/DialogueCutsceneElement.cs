﻿using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Common.Asyncs;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Sheldier.Common.Cutscene
{
    public class DialogueCutsceneElement : SerializedMonoBehaviour, ICutsceneElement
    {
        public bool WaitElement => waitElement;
        
        [SerializeField] private bool waitElement;
        [SerializeField] private DataReference[] actorsInDialogue;
        [SerializeField] private DataReference dialogue;

        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private CutsceneInternalData _data;
        private bool _isFinished;

        public void SetDependencies(CutsceneInternalData data)
        {
            _data = data;
            _dynamicConfigDatabase = data.DynamicConfigDatabase;
        }

        public async Task PlayCutScene()
        {
            string[] actors = new string[actorsInDialogue.Length];
            for (int i = 0; i < actorsInDialogue.Length; i++)
            {
                if (actorsInDialogue[i].Reference == GameplayConstants.CURRENT_PLAYER)
                {
                    actors[i] = _data.CurrentPlayer.Guid;
                    continue;
                }

                if (!_data.SceneActorsDatabase.ContainsKey(actorsInDialogue[i].Reference))
                    return;
                _isFinished = false;
                actors[i] = _dynamicConfigDatabase.Get(actorsInDialogue[i].Reference).Guid;

                var playRequest = new DialoguePlayRequest()
                {
                    DialogueId = dialogue.Reference,
                    ActorsGuidsInDialogue = actors,
                    OnDialogueCompleted = OnFinished
                };
                MessageBroker.Default.Publish(playRequest);
                await AsyncWaitersFactory.WaitUntil(() => _isFinished);
            }
        }

        private void OnFinished() => _isFinished = true;
    }
}