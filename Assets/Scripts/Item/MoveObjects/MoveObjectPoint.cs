using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Item
{
    public class MoveObjectPoint : MonoBehaviour, IInteractReceiver, IMoveObjectPoint
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;

        [SerializeField] private MoveObject moveObject;
        
        private bool _isMoving;

        public void OnEntered()
        {
        }

        public bool OnInteracted(Actor actor)
        {
            if (!actor.StateDataModule.IsItemExists(GameplayConstants.MOVES_OBJECTS_STATE_DATA))
                return false;
            _isMoving = !_isMoving; 
            actor.StateDataModule.Get(GameplayConstants.MOVES_OBJECTS_STATE_DATA).SetState(_isMoving);
            if (_isMoving)
            {
                actor.transform.position = transform.position;
                moveObject.DisableAllExcept(this);
            }
            else
            {
                moveObject.EnableAllExcept(this);
            }
            transform.parent.SetParent(_isMoving ? actor.transform : null);
            return false;
        }

        public void OnExit()
        {
        }
    }
}
