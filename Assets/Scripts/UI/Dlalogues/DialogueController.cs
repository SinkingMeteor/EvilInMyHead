using System;
using System.Collections;
using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class DialogueController : SerializedMonoBehaviour, IUIInitializable
    {
        
        [SerializeField] private DialogueChoiceViewer choiceViewer;
        [SerializeField] private SpeechCloudController speechCloudController;
        
        private DialoguesProvider _dialoguesProvider;
        private UIStatesController _statesController;
        private IDialoguesInputProvider _dialoguesInputProvider;

        private Database<DynamicNumericalEntityStatsCollection> _dynamicNumericalStatsDatabase;
        private ILocalizationProvider _localizationProvider;
        private IDialogueReplica _currentReplica;
        private Actor[] _actorsInDialogue;
        private Coroutine _waitCoroutine;
        private Action _onCompleteCallback;
        private CameraHandler _cameraHandler;

        public void Initialize()
        {
            _dialoguesProvider.OnDialogueLoaded += StartDialogue;
            speechCloudController.Initialize();
            choiceViewer.Initialize();
        }

        [Inject]
        private void InjectDependencies(DialoguesProvider dialoguesProvider, UIStatesController statesController, IDialoguesInputProvider dialoguesInputProvider,
            SpeechCloudPool speechCloudPool, ILocalizationProvider localizationProvider, IInputBindIconProvider bindIconProvider, IFontProvider fontProvider,
            ChoiceSlotPool choiceSlotPool, CameraHandler cameraHandler, Database<DynamicNumericalEntityStatsCollection> dynamicNumericalStatsDatabase)
        {
            _dynamicNumericalStatsDatabase = dynamicNumericalStatsDatabase;
            _cameraHandler = cameraHandler;
            _dialoguesInputProvider = dialoguesInputProvider;
            _statesController = statesController;
            _dialoguesProvider = dialoguesProvider;
            _localizationProvider = localizationProvider;
            speechCloudController.SetDependencies(speechCloudPool, localizationProvider);
            choiceViewer.SetDependencies(_dialoguesInputProvider, localizationProvider, bindIconProvider, this, choiceSlotPool);
        }

        public void SetNext(IDialogueReplica dialogueReplica)
        {
            _currentReplica = dialogueReplica;
            ProcessReplica();
        }
        public void StartDialogue(DialogueSystemGraph dialogue, Actor[] actors, Action callback)
        {
            _onCompleteCallback = callback;
            EnableState();
            _currentReplica = dialogue.StartReplica;
            _actorsInDialogue = actors;
            ProcessReplica();
        }

        private void ProcessReplica()
        {
            speechCloudController.DeactivateCurrentCloud();
            if (_waitCoroutine != null)
                StopCoroutine(_waitCoroutine);
            
            if (_currentReplica == null)
            {
                DisableState();
                _cameraHandler.SetFollowTarget(_actorsInDialogue[0].transform);
                _onCompleteCallback?.Invoke();
                return;
            }
            Actor actor = _actorsInDialogue[(int) _currentReplica.Person];
            var cloudLifetime = _dynamicNumericalStatsDatabase.Get(actor.Guid).Get(StatsConstants.ACTOR_TYPE_SPEED_STAT).BaseValue
                * _localizationProvider.LocalizedText[_currentReplica.Replica].Length + _currentReplica.Delay;

            if (_currentReplica.Choices.Count > 1)
                choiceViewer.Activate(_currentReplica.Choices, cloudLifetime);
            else
                _waitCoroutine = StartCoroutine(WaitCloudCoroutine(cloudLifetime));

            _cameraHandler.SetFollowTarget(actor.transform);
            speechCloudController.RevealCloud(_currentReplica, actor);

            _currentReplica = _currentReplica.Choices.Count == 0 ? null : _currentReplica.Choices[^1].Next;


        }

        private void EnableState()
        {
            _statesController.Add(UIType.Dialogue);
        }

        private void DisableState()
        {
            _currentReplica = null;
            _statesController.Remove(UIType.Dialogue);
        }
        public void Dispose()
        {
            _dialoguesProvider.OnDialogueLoaded -= StartDialogue;
        }
        
        private IEnumerator WaitCloudCoroutine(float timeLeft)
        {
            yield return new WaitForSeconds(timeLeft);
            ProcessReplica();
        }
    }
}