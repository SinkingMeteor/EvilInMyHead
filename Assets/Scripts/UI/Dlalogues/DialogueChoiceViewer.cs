using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class DialogueChoiceViewer : SerializedMonoBehaviour, ILocalizationListener, IDeviceListener
    {
        [OdinSerialize] private IUIStateAnimationAppearing[] appearingAnimations;
        [OdinSerialize] private IUIStateAnimationDisappearing[] disappearingAnimations;
        [SerializeField] private Image timerImage;
        [SerializeField] private RectTransform slotsParent;

        private List<ChoiceSlot> choiceSlots;
        private IDialoguesInputProvider _inputProvider;
        private IInputBindIconProvider _bindIconProvider;
        private ILocalizationProvider _localizationProvider;
        private IReadOnlyList<ReplicaChoice> _currentChoices;
        private DialogueController _dialogueController;
        private Coroutine _waitingCoroutine;
        private IFontProvider _fontProvider;
        private ChoiceSlotPool _choiceSlotPool;


        public void Initialize()
        {
            choiceSlots = new List<ChoiceSlot>();
            for (int i = 0; i < appearingAnimations.Length; i++)
                appearingAnimations[i].Initialize();
            for (int i = 0; i < disappearingAnimations.Length; i++)
                disappearingAnimations[i].Initialize();
        }
        
        public void SetDependencies(IDialoguesInputProvider inputProvider, ILocalizationProvider localizationProvider, IInputBindIconProvider bindIconProvider,
            DialogueController dialogueController, ChoiceSlotPool choiceSlotPool)
        {
            _choiceSlotPool = choiceSlotPool;
            _dialogueController = dialogueController;
            _localizationProvider = localizationProvider;
            _bindIconProvider = bindIconProvider;
            _inputProvider = inputProvider;
        }
        public async void Activate(IReadOnlyList<ReplicaChoice> currentReplicaChoices, float cloudLifetime)
        {
            _currentChoices = currentReplicaChoices;
            timerImage.fillAmount = 0.25f;

            Task[] tasks = new Task[appearingAnimations.Length];
            for (int i = 0; i < appearingAnimations.Length; i++)
                tasks[i] = appearingAnimations[i].PlayAnimation();

            await Task.WhenAll(tasks);
            
            _localizationProvider.AddListener(this);
            _bindIconProvider.AddListener(this);
            
            _inputProvider.LeftChoice.OnPressed += OnLeftChoicePressed;
            _inputProvider.UpperChoice.OnPressed += OnUpperChoicePressed;
            _inputProvider.RightChoice.OnPressed += OnRightChoicePressed;
            _inputProvider.LowerChoice.OnPressed += OnLowerChoicePressed;

            _waitingCoroutine = StartCoroutine(WaitingCoroutine(cloudLifetime - 0.5f));

            InstantiateChoices(currentReplicaChoices);
        }



        public void OnLanguageChanged()
        {
            for (int i = 0; i < choiceSlots.Count; i++)
                choiceSlots[i].SetText(_localizationProvider.LocalizedText[_currentChoices[i].Choice]);
        }

        public void OnDeviceChanged()
        {
            for (int i = 0; i < choiceSlots.Count; i++)
                choiceSlots[i].SetBindIcon(_bindIconProvider.GetActionInputSprite(GetAction(i)));

        }

        private void InstantiateChoices(IReadOnlyList<ReplicaChoice> currentReplicaChoices)
        {
            for (int i = 0; i < currentReplicaChoices.Count; i++)
            {
                ChoiceSlot slot = _choiceSlotPool.GetFromPool();
                slot.transform.SetParent(slotsParent);
                slot.transform.localScale = Vector3.one;
                slot.SetText(_localizationProvider.LocalizedText[currentReplicaChoices[i].Choice]);
                slot.SetBindIcon(_bindIconProvider.GetActionInputSprite(GetAction(i)));
                choiceSlots.Add(slot);
                slot.Activate(_currentChoices[i].Next);
            }
        }
        private async void Deactivate()
        {
            Task[] tasks = new Task[choiceSlots.Count];
            
            for(int i =0; i < choiceSlots.Count; i++)
                tasks[i] = choiceSlots[i].Deactivate();
            
            await Task.WhenAll(tasks);

            choiceSlots.Clear();
#pragma warning disable CS4014
            for (int i = 0; i < disappearingAnimations.Length; i++)
                disappearingAnimations[i].PlayAnimation();
#pragma warning restore CS4014
            _localizationProvider.RemoveListener(this);
            _bindIconProvider.RemoveListener(this);
        }
        
        private void OnLowerChoicePressed() => ApplyChoice(0);
        private void OnLeftChoicePressed() => ApplyChoice(1);
        private void OnUpperChoicePressed() => ApplyChoice(2);
        private void OnRightChoicePressed() => ApplyChoice(3);


        private async void ApplyChoice(int index)
        {
            if(index >= choiceSlots.Count)
                return;
            if (choiceSlots[index].Next == null)
                return;
            if(_waitingCoroutine != null)
                StopCoroutine(_waitingCoroutine);
            
            _inputProvider.LeftChoice.OnPressed -= OnLeftChoicePressed;
            _inputProvider.UpperChoice.OnPressed -= OnUpperChoicePressed;
            _inputProvider.RightChoice.OnPressed -= OnRightChoicePressed;
            _inputProvider.LowerChoice.OnPressed -= OnLowerChoicePressed;
            
            IDialogueReplica nextReplica = choiceSlots[index].Next;
            _dialogueController.SetNext(nextReplica);                        
            await choiceSlots[index].Select();
            Deactivate();
        }
        private InputActionType GetAction(int index)
        {
            return index switch
            {
                0 => InputActionType.DialoguesLowerChoice,
                1 => InputActionType.DialoguesLeftChoice,
                2 => InputActionType.DialoguesUpperChoice,
                3 => InputActionType.DialoguesRightChoice,
                _ => throw new ArgumentOutOfRangeException(nameof(gameObject), index, null)
            };
        }

        private IEnumerator WaitingCoroutine(float delay)
        {
            float timeLeft = delay;
            while (timeLeft > 0.0f)
            {
                timeLeft -= Time.deltaTime;
                timerImage.fillAmount = Mathf.InverseLerp(0.0f, delay, timeLeft) / 4.0f;
                yield return null;
            }
            ApplyChoice(UnityEngine.Random.Range(0, _currentChoices.Count));
        }

    }
}