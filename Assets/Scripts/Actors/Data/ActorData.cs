using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/ActorData", fileName = "ActorData")]
    public class ActorData : BaseScriptableObject
    {
        public ActorMovementConfig MovementConfig => movementConfig;
        
        [SerializeField] private ActorMovementConfig movementConfig;
    }
}