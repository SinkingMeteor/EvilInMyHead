using System;
using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Data;
using Sheldier.Graphs.DialogueSystem;
using UnityEngine;

namespace Sheldier.Common
{
    public class DialoguesProvider
    {
        public event Action<DialogueSystemGraph, Actor[], Action> OnDialogueLoaded;
        
        private Dictionary<string, DialoguePointer> _pointers;
        private ActorSpawner _spawner;
        private readonly Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private readonly SceneActorsDatabase _sceneActorsDatabase;

        public DialoguesProvider(SceneActorsDatabase sceneActorsDatabase, Database<ActorDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _sceneActorsDatabase = sceneActorsDatabase;
        }
        
        public void Initialize()
        {
            _pointers = new Dictionary<string, DialoguePointer>()
            {
                {"Fred", new DialoguePointer("Dialogues/Fred/TestDialogue")}
            };
        }

        public void FindDialogue(Actor dialogueInitiator, Actor dialogueTarget)
        {
            var typeName = _dynamicConfigDatabase.Get(dialogueTarget.Guid).TypeName;
            DialogueSystemGraph graph = _pointers[typeName].GetDialogue();
            Actor[] actorsInDialogues = new Actor[2 + (graph.AdditionalPersons?.Length ?? 0)];
            actorsInDialogues[0] = dialogueInitiator;
            actorsInDialogues[1] = dialogueTarget;
            if (graph.AdditionalPersons != null)
                for (int i = 0; i < graph.AdditionalPersons.Length; i++)
                {
                    string typeID = graph.AdditionalPersons[i].Reference;
                    if (!_sceneActorsDatabase.ContainsKey(typeID))
                        return;
                    actorsInDialogues[i + 2] = _sceneActorsDatabase.GetFirst(typeID);
                }

            actorsInDialogues[0].LockInput();
            OnDialogueLoaded?.Invoke(graph, actorsInDialogues, () => actorsInDialogues[0].UnlockInput());
        }

        public void StartDialogue(DialogueSystemGraph graph, Actor[] actors, Action onCompleted)
        {
            OnDialogueLoaded?.Invoke(graph, actors, onCompleted);
        }
    }

    public class DialoguePointer
    {
        private readonly string _path;
        private int _currentIndex;

        public DialoguePointer(string path)
        {
            _path = path;
            _currentIndex = 0;
        }

        public void SetIndex(int index)
        {
            _currentIndex = index;
        }

        public DialogueSystemGraph GetDialogue()
        {
            return Resources.Load<DialogueSystemGraph>(_path + _currentIndex);
        }
    }
}