using UnityEngine;

namespace Sheldier.Common
{
    public class CameraSideMover
    {
        private LateTickHandler _lateTickHandler;


        private IInputProvider _inputProvider;
        private Camera _camera;
        private CameraFollower _cameraFollower;

        public void SetDependencies(Camera camera, IInputProvider inputProvider, CameraFollower cameraFollower)
        {
            _inputProvider = inputProvider;
            _camera = camera;
            _cameraFollower = cameraFollower;
        }

        public void LateTick()
        {
            var cursorMousePosition = _inputProvider.CursorScreenDirection;
           _camera.transform.position = _cameraFollower.TargetPosition + new Vector3(cursorMousePosition.x, cursorMousePosition.y, 0.0f);
        }
    }
}