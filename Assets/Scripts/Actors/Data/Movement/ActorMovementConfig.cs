using Sheldier.Common.Audio;
using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(fileName = "ActorMovementConfig", menuName = "Sheldier/Actor/MovementConfig")]
    public class ActorMovementConfig : BaseScriptableObject
    {
        [SerializeField] [Range(0.5f, 5f)] private float speed;
        [SerializeField] private AudioUnit movementSound;
        
        public float Speed => speed;
        public AudioUnit MovementSound => movementSound;
    }

}
