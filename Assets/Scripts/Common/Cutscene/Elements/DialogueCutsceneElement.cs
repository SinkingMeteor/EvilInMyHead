using System.Threading.Tasks;
using Sheldier.Actors.Data;
using Sheldier.Common.Asyncs;
using Sheldier.Constants;
using Sheldier.Data;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Cutscene
{
    public class DialogueCutsceneElement : SerializedMonoBehaviour, ICutsceneElement
    {
        public bool WaitElement => waitElement;
        
        [SerializeField] private bool waitElement;
        [SerializeField] private DataReference[] actorsInDialogue;
        [SerializeField] private DataReference dialogue;

        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private ScenePlayerController _scenePlayerController;
        private SceneActorsDatabase _sceneActorsDatabase;
        private bool _isFinished;

        [Inject]
        private void InjectDependencies(Database<ActorDynamicConfigData> dynamicConfigDatabase,
                                        SceneActorsDatabase sceneActorsDatabase,
                                        ScenePlayerController scenePlayerController)
        {
            _sceneActorsDatabase = sceneActorsDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _scenePlayerController = scenePlayerController;
        }

        public async Task PlayCutScene()
        {
            string[] actors = new string[actorsInDialogue.Length];
            for (int i = 0; i < actorsInDialogue.Length; i++)
            {
                if (actorsInDialogue[i].Reference == GameplayConstants.CURRENT_PLAYER)
                {
                    actors[i] = _scenePlayerController.ControlledActorGuid;
                    continue;
                }

                if (!_sceneActorsDatabase.ContainsKey(actorsInDialogue[i].Reference))
                    return;
                _isFinished = false;
                actors[i] = _dynamicConfigDatabase.Get(actorsInDialogue[i].Reference).Guid;
            }
            var playRequest = new DialoguePlayRequest()
            {
                DialogueId = dialogue.Reference,
                ActorsGuidsInDialogue = actors,
                OnDialogueCompleted = OnFinished
            };
            MessageBroker.Default.Publish(playRequest);
            await AsyncWaitersFactory.WaitUntil(() => _isFinished);
        }

        private void OnFinished() => _isFinished = true;
    }
}