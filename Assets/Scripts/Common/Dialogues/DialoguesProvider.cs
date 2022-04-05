﻿using System;
using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Graphs.DialogueSystem;
using UnityEngine;

namespace Sheldier.Common
{
    public class DialoguesProvider
    {
        public event Action<DialogueSystemGraph, Actor[], Action> OnDialogueLoaded;
        
        private Dictionary<string, DialoguePointer> _pointers;
        private ActorSpawner _spawner;

        public void Initialize()
        {
            _pointers = new Dictionary<string, DialoguePointer>()
            {
                {"Fred", new DialoguePointer("Dialogues/Fred/TestDialogue")}
            };
        }

        public void SetDependencies(ActorSpawner spawner)
        {
            _spawner = spawner;
        }
        public void FindDialogue(Actor dialogueInitiator, Actor dialogueTarget)
        {
            DialogueSystemGraph graph = _pointers[dialogueTarget.Type].GetDialogue();
            Actor[] actorsInDialogues = new Actor[2 + (graph.AdditionalPersons?.Length ?? 0)];
            actorsInDialogues[0] = dialogueInitiator;
            actorsInDialogues[1] = dialogueTarget;
            if (graph.AdditionalPersons != null)
                for (int i = 0; i < graph.AdditionalPersons.Length; i++)
                {
                    string typeID = graph.AdditionalPersons[i].Reference;
                    if (!_spawner.ActorsOnScene.ContainsKey(typeID))
                        return;
                    actorsInDialogues[i + 2] = _spawner.ActorsOnScene[typeID][0];
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