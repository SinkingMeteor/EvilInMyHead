﻿using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Common.Asyncs;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Common.Cutscene
{
    public class DialogueCutsceneElement : SerializedMonoBehaviour, ICutsceneElement
    {
        public bool WaitElement => waitElement;
        
        [SerializeField] private bool waitElement;
        [SerializeField] private ActorType[] actorsInDialogue;
        [SerializeField] private DialogueSystemGraph dialogue;
        
        private CutsceneInternalData _data;
        private bool _isFinished;

        public void SetDependencies(CutsceneInternalData data)
        {
            _data = data;
        }

        public async Task PlayCutScene()
        {
            Actor[] actors = new Actor[actorsInDialogue.Length];
            for (int i = 0; i < actorsInDialogue.Length; i++)
            {
                if (actorsInDialogue[i] == ActorType.CurrentPlayer)
                {
                    actors[i] = _data.CurrentPlayer;
                    continue;
                }

                if (!_data.ActorSpawner.ActorsOnScene.ContainsKey(actorsInDialogue[i]))
                    return;
                _isFinished = false;
                actors[i] = _data.ActorSpawner.ActorsOnScene[actorsInDialogue[i]][0];
                _data.DialoguesProvider.StartDialogue(dialogue, actors, OnFinished);
                await AsyncWaitersFactory.WaitUntil(() => _isFinished);
            }
        }

        private void OnFinished() => _isFinished = true;
    }
}