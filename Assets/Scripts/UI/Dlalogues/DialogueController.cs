using System;
using System.Collections;
using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Graphs.DialogueSystem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class DialogueController : SerializedMonoBehaviour, IUIInitializable
    {
        [SerializeField] private DialogueChoiceViewer choiceViewer;
        [SerializeField] private SpeechCloudController speechCloudController;

        private Database<DynamicNumericalEntityStatsCollection> _dynamicNumericalStatsDatabase;
        private Database<DynamicStringEntityStatsCollection> _dynamicStringStatsDatabase;
        private Database<DialogueStaticData> _dialogueStaticDatabase;
        private AssetProvider<DialogueSystemGraph> _dialogueLoader;

        private IDialoguesInputProvider _dialoguesInputProvider;
        private ILocalizationProvider _localizationProvider;
        private SceneActorsDatabase _sceneActorsDatabase;
        private UIStatesController _statesController;
        private IDialogueReplica _currentReplica;
        private ICameraFollower _cameraHandler;

        private string[] _actorsGuidsInDialogue;
        private string _currentDialogueType;
        private Action _onCompleteCallback;
        
        private IDisposable _playDialogueEvent;
        private Coroutine _waitCoroutine;
        public void Initialize()
        {
            _playDialogueEvent = MessageBroker.Default.Receive<DialoguePlayRequest>().Subscribe(StartDialogue);
            choiceViewer.OnNextReplica += SetNext;
            speechCloudController.Initialize();
            choiceViewer.Initialize();
        }

        [Inject]
        private void InjectDependencies(UIStatesController statesController,
                                        ILocalizationProvider localizationProvider, 
                                        ICameraFollower cameraHandler,
                                        Database<DynamicNumericalEntityStatsCollection> dynamicNumericalStatsDatabase,
                                        Database<DialogueStaticData> dialogueStaticDatabase,
                                        AssetProvider<DialogueSystemGraph> dialogueLoader,
                                        Database<DynamicStringEntityStatsCollection> dynamicStringStatsDatabase,
                                        SceneActorsDatabase sceneActorsDatabase,
                                        IDialoguesInputProvider dialoguesInputProvider)
        {
            _dynamicNumericalStatsDatabase = dynamicNumericalStatsDatabase;
            _dynamicStringStatsDatabase = dynamicStringStatsDatabase;
            _dialoguesInputProvider = dialoguesInputProvider;
            _dialogueStaticDatabase = dialogueStaticDatabase;
            _localizationProvider = localizationProvider;
            _sceneActorsDatabase = sceneActorsDatabase;
            _statesController = statesController;
            _dialogueLoader = dialogueLoader;
            _cameraHandler = cameraHandler;
        }

        private void SetNext(IDialogueReplica dialogueReplica)
        {
            _currentReplica = dialogueReplica;
            UnlockPassButton();
            ProcessReplica();
        }

        private void StartDialogue(DialoguePlayRequest request)
        {
            _onCompleteCallback = request.OnDialogueCompleted;
            EnableState();
            _currentDialogueType = request.DialogueId;
            _currentReplica = _dialogueLoader.Get(request.DialogueId).StartReplica;
            _actorsGuidsInDialogue = request.ActorsGuidsInDialogue;
            UnlockPassButton();
            ProcessReplica();
        }

        private void ProcessReplica()
        {
            speechCloudController.DeactivateCurrentCloud();
            if (_waitCoroutine != null)
                StopCoroutine(_waitCoroutine);
            
            if (_currentReplica == null)
            {
                CompleteDialogue();
                return;
            }
            Actor actor = _sceneActorsDatabase.Get(_actorsGuidsInDialogue[(int) _currentReplica.Person]);
            var cloudLifetime = _dynamicNumericalStatsDatabase.Get(actor.Guid).Get(StatsConstants.ACTOR_TYPE_SPEED_STAT).BaseValue
                * _localizationProvider.LocalizedText[_currentReplica.Replica].Length + _currentReplica.Delay;

            if (_currentReplica.Choices.Count > 1)
            {
                LockPassButton();
                choiceViewer.Activate(_currentReplica.Choices, cloudLifetime);
            }
            else
                _waitCoroutine = StartCoroutine(WaitCloudCoroutine(cloudLifetime));

            _cameraHandler.SetFollowTarget(actor.transform);
            speechCloudController.RevealCloud(_currentReplica, actor);

            _currentReplica = _currentReplica.Choices.Count == 0 ? null : _currentReplica.Choices[^1].Next;
        }

        private void LockPassButton() => _dialoguesInputProvider.PassReplicaButton.OnPressed -= ProcessReplica;
        private void UnlockPassButton() => _dialoguesInputProvider.PassReplicaButton.OnPressed += ProcessReplica;
        
        private void CompleteDialogue()
        {
            ChangeNextDialogue();
            LockPassButton();
            DisableState();
            _cameraHandler.SetFollowTarget(_sceneActorsDatabase.Get(_actorsGuidsInDialogue[0]).transform);
            _onCompleteCallback?.Invoke();
        }

        private void ChangeNextDialogue()
        {
            var dialogueData = _dialogueStaticDatabase.Get(_currentDialogueType);
            if (dialogueData.NextDialogueName == "None")
                return;
            
            _dynamicStringStatsDatabase.Get(_actorsGuidsInDialogue[1]).Get(StatsConstants.ACTOR_CURRENT_DIALOGUE_STAT).Value = dialogueData.NextDialogueName;
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
            choiceViewer.OnNextReplica -= SetNext;
            _playDialogueEvent.Dispose();
        }
        
        private IEnumerator WaitCloudCoroutine(float timeLeft)
        {
            yield return new WaitForSeconds(timeLeft);
            ProcessReplica();
        }
    }
}