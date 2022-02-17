using Sheldier.Common.Audio;
using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(fileName = "ActorMovementConfig", menuName = "Sheldier/ActorsData/MovementConfig")]
    public class ActorActorMovementConfig : BaseScriptableObject, IActorMovementDataProvider
    {
        [SerializeField] [Range(0.5f, 5f)] private float speed;
        [SerializeField] private AudioUnit movementSound;
        
        public float Speed => speed;
        public AudioUnit MovementSound => movementSound;
    }

}
