using Sheldier.Common;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorTransformHandler
    {
        public ActorDirectionView CurrentDirectionView => _currentDirectionView;
        private ActorDirectionView _currentDirectionView;

        private Transform _actorTransform;
        private Camera _camera;
        private ActorInputController _actorInputController;
        

        public void SetDependencies(Transform actorTransform, ActorInputController actorInputController)
        {
            _actorInputController = actorInputController;
            _camera = Camera.main;
            _actorTransform = actorTransform;
        }

        public void Tick()
        {
            CalculateViewDirection();
        }

        private void CalculateViewDirection()
        {
            if (_camera == null)
                return;
            Vector2 mouseWorld = _camera.ScreenToWorldPoint(_actorInputController.CurrentInputProvider.CursorPosition);
            Vector2 actorPos = new Vector2(_actorTransform.position.x, _actorTransform.position.y);
            var directionFromActorToMouse = (mouseWorld - actorPos).normalized;
            var looksToRight = Vector2.Dot(Vector2.right, directionFromActorToMouse);
            _actorTransform.localScale = new Vector3(looksToRight >= 0 ? 1 : -1, _actorTransform.localScale.y,
                _actorTransform.localScale.z);
            _currentDirectionView = directionFromActorToMouse.GetViewDirection();
        }
    }
}