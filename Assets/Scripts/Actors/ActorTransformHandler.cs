using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorTransformHandler
    {
        private ActorDirectionView _previousDirectionView;

        private Transform _actorTransform;
        private ActorInputController _actorInputController;
        

        public void SetDependencies(Transform actorTransform, ActorInputController actorInputController)
        {
            _actorInputController = actorInputController;
            _actorTransform = actorTransform;
        }

        public ActorDirectionView CalculateViewDirection()
        {
            Vector2 cursorScreenDirection = _actorInputController.CurrentInputProvider.CursorScreenDirection.normalized;
            if (cursorScreenDirection.sqrMagnitude < Mathf.Epsilon)
                return _previousDirectionView;
            
            
            var looksToRight = Vector2.Dot(Vector2.right, cursorScreenDirection);
            _actorTransform.localScale = new Vector3(looksToRight >= 0 ? 1 : -1, _actorTransform.localScale.y,
                _actorTransform.localScale.z);
            _previousDirectionView = cursorScreenDirection.GetViewDirection();
            return _previousDirectionView;
        }

        public ActorDirectionView CalculateMovementDirection()
        {
            Vector2 movementDirection = _actorInputController.CurrentInputProvider.MovementDirection.normalized;
            var movesToRight = Vector2.Dot(Vector2.right, movementDirection);
            _actorTransform.localScale = new Vector3(movesToRight >= 0 ? 1 : -1, _actorTransform.localScale.y,
                _actorTransform.localScale.z);
            _previousDirectionView = movementDirection.GetViewDirection();
            return _previousDirectionView;
        }
    }
}