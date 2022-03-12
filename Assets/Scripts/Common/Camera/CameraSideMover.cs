using UnityEngine;

namespace Sheldier.Common
{
    public class CameraSideMover
    {
        private IInputProvider _inputProvider;
        private Camera _camera;
        private CameraFollower _cameraFollower;

        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }
        
        public void SetDependencies(IInputProvider inputProvider, CameraFollower cameraFollower)
        {
            _inputProvider = inputProvider;
            _cameraFollower = cameraFollower;
        }

        public void LateTick()
        {
            var cursorMousePosition = _inputProvider.CursorScreenCenterDirection;
           _camera.transform.position = _cameraFollower.TargetPosition + new Vector3(cursorMousePosition.x, cursorMousePosition.y, 0.0f);
        }
        
    }
}