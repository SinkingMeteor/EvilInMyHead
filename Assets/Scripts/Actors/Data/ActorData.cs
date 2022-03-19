using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/ActorData", fileName = "ActorData")]
    public class ActorData : BaseScriptableObject
    {
        public ActorMovementConfig MovementConfig => movementConfig;
        public ActorDialogueConfig DialogueConfig => dialogueConfig;
        
        [SerializeField] private ActorMovementConfig movementConfig;
        [SerializeField] private ActorDialogueConfig dialogueConfig;
    }
}