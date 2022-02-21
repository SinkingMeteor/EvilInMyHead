using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorPhysicsKeeper : MonoBehaviour
    {
        public Animator Animator => _animator;
        public Rigidbody2D Rigidbody2D => _rigidbody;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
    }
}