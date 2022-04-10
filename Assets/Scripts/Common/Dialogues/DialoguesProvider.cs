using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Graphs.DialogueSystem;
using UniRx;
using UnityEngine;

namespace Sheldier.Common
{
    public class DialoguesProvider
    {
        private ActorSpawner _spawner;
        private readonly SceneActorsDatabase _sceneActorsDatabase;
        
        private readonly Database<DynamicStringEntityStatsCollection> _dynamicStatDatabase;
        private readonly AssetProvider<DialogueSystemGraph> _dialoguesLoader;

        public DialoguesProvider(SceneActorsDatabase sceneActorsDatabase,
                                 AssetProvider<DialogueSystemGraph> dialoguesLoader,
                                 Database<DynamicStringEntityStatsCollection> dynamicStatDatabase)
        {
            _dynamicStatDatabase = dynamicStatDatabase;
            _dialoguesLoader = dialoguesLoader;
            _sceneActorsDatabase = sceneActorsDatabase;
        }
        public void FindDialogue(Actor dialogueInitiator, Actor dialogueTarget)
        {
            var dialogueID = _dynamicStatDatabase.Get(dialogueTarget.Guid).Get(StatsConstants.ACTOR_CURRENT_DIALOGUE_STAT).Value;
            var graph = _dialoguesLoader.Get(dialogueID);
            string[] actorsInDialogues = new string[2 + (graph.AdditionalPersons?.Length ?? 0)];
            actorsInDialogues[0] = dialogueInitiator.Guid;
            actorsInDialogues[1] = dialogueTarget.Guid;
            
            if (graph.AdditionalPersons != null)
                for (int i = 0; i < graph.AdditionalPersons.Length; i++)
                {
                    string typeID = graph.AdditionalPersons[i].Reference;
                    if (!_sceneActorsDatabase.ContainsKey(typeID))
                        return;
                    actorsInDialogues[i + 2] = _sceneActorsDatabase.GetFirst(typeID).Guid;
                }

            dialogueInitiator.LockInput();

            var playRequest = new DialoguePlayRequest()
            {
                ActorsGuidsInDialogue = actorsInDialogues,
                DialogueId = dialogueID,
                OnDialogueCompleted = dialogueInitiator.UnlockInput
            };
            
            MessageBroker.Default.Publish(playRequest);
        }
    }
}