﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.UI
{
    public class DialogueChoiceViewer : SerializedMonoBehaviour, ILocalizationListener, IDeviceListener
    {

        [OdinSerialize] private IUIStateAnimationAppearing[] appearingAnimations;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;
        [OdinSerialize] private Dictionary<ChoiceType, ChoiceSlot> choiceSlots;
        
        private IDialoguesInputProvider _inputProvider;
        private IInputBindIconProvider _bindIconProvider;
        private ILocalizationProvider _localizationProvider;
        private IReadOnlyList<ReplicaChoice> _currentChoices;
        private DialogueController _dialogueController;
        private Coroutine _waitingCoroutine;

        public void SetDependencies(IDialoguesInputProvider inputProvider, ILocalizationProvider localizationProvider, IInputBindIconProvider bindIconProvider,
            DialogueController dialogueController)
        {
            _dialogueController = dialogueController;
            _localizationProvider = localizationProvider;
            _bindIconProvider = bindIconProvider;
            _inputProvider = inputProvider;
        }
        public void Activate(IReadOnlyList<ReplicaChoice> currentReplicaChoices, float cloudLifetime)
        {
            _currentChoices = currentReplicaChoices;
            for (int i = 0; i < appearingAnimations.Length; i++)
                appearingAnimations[i].PlayAnimation();
            
            _localizationProvider.AddListener(this);
            _bindIconProvider.AddListener(this);
            
            _inputProvider.LeftChoice.OnPressed += OnLeftChoicePressed;
            _inputProvider.UpperChoice.OnPressed += OnUpperChoicePressed;
            _inputProvider.RightChoice.OnPressed += OnRightChoicePressed;
            _inputProvider.LowerChoice.OnPressed += OnLowerChoicePressed;

            _waitingCoroutine = StartCoroutine(WaitingCoroutine(cloudLifetime - 0.5f));
            
            FillText();
            SetBindings();
            SetNextReplica();
        }

        private void SetNextReplica()
        {
            for (int i = 0; i < _currentChoices.Count; i++)
            {
                ChoiceSlot slot = choiceSlots[(ChoiceType) i];
                slot.Activate(_currentChoices[i].Next);
            }
        }

        private void SetBindings()
        {
            for (int i = 0; i < _currentChoices.Count; i++)
            {
                ChoiceType choice = (ChoiceType) i;
                ChoiceSlot slot = choiceSlots[choice];
                slot.SetBindIcon(_bindIconProvider.GetActionInputSprite(GetAction(choice)));
            }
        }
  
        private void FillText()
        {
            for (int i = 0; i < _currentChoices.Count; i++)
            {
                ChoiceSlot slot = choiceSlots[(ChoiceType) i];
                slot.SetText(_localizationProvider.LocalizedText[_currentChoices[i].Choice]);
            }
        }
        public void OnLanguageChanged() => FillText();
        public void OnDeviceChanged() => SetBindings();

        private void Deactivate()
        {

            
            _inputProvider.LeftChoice.OnPressed -= OnLeftChoicePressed;
            _inputProvider.UpperChoice.OnPressed -= OnUpperChoicePressed;
            _inputProvider.RightChoice.OnPressed -= OnRightChoicePressed;
            _inputProvider.LowerChoice.OnPressed -= OnLowerChoicePressed;
            
            disappearingAnimation.PlayAnimation();

            foreach (var choiceSlot in choiceSlots) 
                choiceSlot.Value.Reset();

            _localizationProvider.RemoveListener(this);
            _bindIconProvider.RemoveListener(this);
        }
        private void OnLowerChoicePressed() => ApplyChoice(ChoiceType.Lower);

        private void OnRightChoicePressed() => ApplyChoice(ChoiceType.Right);

        private void OnUpperChoicePressed() => ApplyChoice(ChoiceType.Upper);

        private void OnLeftChoicePressed() => ApplyChoice(ChoiceType.Left);

        private async void ApplyChoice(ChoiceType choiceType)
        {
            if (choiceSlots[choiceType].Next == null)
                return;
            if(_waitingCoroutine != null)
                StopCoroutine(_waitingCoroutine);
            IDialogueReplica nextReplica = choiceSlots[choiceType].Next;
            _dialogueController.SetNext(nextReplica);                        
            await choiceSlots[choiceType].Select();
            Deactivate();
        }
        private InputActionType GetAction(ChoiceType choiceType)
        {
            return choiceType switch
            {
                ChoiceType.Left => InputActionType.DialoguesLeftChoice,
                ChoiceType.Upper => InputActionType.DialoguesUpperChoice,
                ChoiceType.Right => InputActionType.DialoguesRightChoice,
                ChoiceType.Lower => InputActionType.DialoguesLowerChoice,
                _ => throw new ArgumentOutOfRangeException(nameof(choiceType), choiceType, null)
            };
        }

        private IEnumerator WaitingCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            ApplyChoice((ChoiceType)UnityEngine.Random.Range(0, _currentChoices.Count));
        }
        private enum ChoiceType
        {
            Lower,
            Upper,
            Right,
            Left
        }



    }
}