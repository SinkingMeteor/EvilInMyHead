using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorTransformHandler
    {
        public bool LooksToRight => _lookDot >= 0;
        
        private ActorDirectionView _previousDirectionView;

        private Transform _actorTransform;
        private ActorInputController _actorInputController;
        private float _lookDot;

        public float GetAngle(Vector2 dir)
        {
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!LooksToRight)
                angle -= 180;
            return angle;
        }
        
        public void SetDependencies(Transform actorTransform, ActorInputController actorInputController)
        {
            _actorInputController = actorInputController;
            _actorTransform = actorTransform;
        }

        public ActorDirectionView CalculateViewDirection()
        {
            Vector2 cursorScreenDirection = _actorInputController.CursorScreenDirection.normalized;
            if (cursorScreenDirection.sqrMagnitude < Mathf.Epsilon)
                return _previousDirectionView;
            
            
            _lookDot = Vector2.Dot(Vector2.right, cursorScreenDirection);
           _actorTransform.localScale = new Vector3(_lookDot >= 0 ? 1 : -1, _actorTransform.localScale.y,
                _actorTransform.localScale.z);
            _previousDirectionView = cursorScreenDirection.GetViewDirection();
            return _previousDirectionView;
        }

        public ActorDirectionView CalculateMovementDirection()
        {
            Vector2 movementDirection = _actorInputController.MovementDirection.normalized;
            var movesToRight = Vector2.Dot(Vector2.right, movementDirection);
            _actorTransform.localScale = new Vector3(movesToRight >= 0 ? 1 : -1, _actorTransform.localScale.y,
                _actorTransform.localScale.z);
            _previousDirectionView = movementDirection.GetViewDirection();
            return _previousDirectionView;
        }
    }
}