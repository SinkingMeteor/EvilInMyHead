using System;
using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Graphs.DialogueSystem;
using UnityEngine;

namespace Sheldier.Common
{
    public class DialoguesProvider
    {
        public event Action<DialogueSystemGraph, Actor[], Action> OnDialogueLoaded;
        
        private Dictionary<ActorType, DialoguePointer> _pointers;
        private ActorSpawner _spawner;

        public void Initialize()
        {
            _pointers = new Dictionary<ActorType, DialoguePointer>()
            {
                {ActorType.Yellow, new DialoguePointer("Dialogues/DialogueYellow")}
            };
        }

        public void SetDependencies(ActorSpawner spawner)
        {
            _spawner = spawner;
        }
        public void FindDialogue(Actor dialogueInitiator, Actor dialogueTarget)
        {
            DialogueSystemGraph graph = _pointers[dialogueTarget.ActorType].GetDialogue();
            Actor[] actorsInDialogues = new Actor[2 + (graph.AdditionalPersons?.Length ?? 0)];
            actorsInDialogues[0] = dialogueInitiator;
            actorsInDialogues[1] = dialogueTarget;
            if (graph.AdditionalPersons != null)
                for (int i = 0; i < graph.AdditionalPersons.Length; i++)
                {
                    ActorType actorType = graph.AdditionalPersons[i];
                    if (!_spawner.ActorsOnScene.ContainsKey(actorType))
                        return;
                    actorsInDialogues[i + 2] = _spawner.ActorsOnScene[actorType][0];
                }

            OnDialogueLoaded?.Invoke(graph, actorsInDialogues, null);
        }

        public void StartDialogue(DialogueSystemGraph graph, Actor[] actors, Action onCompleted)
        {
            OnDialogueLoaded?.Invoke(graph, actors, onCompleted);
        }
        public void SetPointerIndex(ActorType actorType, int index)
        {
            if (!_pointers.ContainsKey(actorType))
                return;
            _pointers[actorType].SetIndex(index);
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