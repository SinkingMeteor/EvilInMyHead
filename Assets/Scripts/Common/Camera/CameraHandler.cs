using UnityEngine;
using Zenject;

namespace Sheldier.Common
{
    public class CameraHandler : ILateTickListener
    {
        public Camera CurrentSceneCamera => _camera;
        
        private Camera _camera;

        private CameraSideMover _cameraSideMover;
        private CameraFollower _cameraFollower;
        
        private LateTickHandler _lateTickHandler;
        private IInputProvider _inputProvider;


        public void InitializeOnScene()
        {
            _camera = Camera.main;
            _cameraFollower.SetCamera(_camera);
            _cameraSideMover.SetCamera(_camera);
            
            _lateTickHandler.AddListener(this);
        }
        
        public void Initialize()
        {
            _cameraFollower = new CameraFollower();
            _cameraFollower.SetCamera(_camera);
            
            _cameraSideMover = new CameraSideMover();
            _cameraSideMover.SetDependencies(_inputProvider, _cameraFollower);
            
        }
        [Inject]
        private void InjectDependencies(LateTickHandler lateTickHandler, IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _lateTickHandler = lateTickHandler;
        }

        public void LateTick()
        {
            _cameraFollower.LateTick();
            _cameraSideMover.LateTick();
        }

        public void SetFollowTarget(Transform transform)
        {
            _cameraFollower.SetNewTarget(transform);
        }
        public void OnSceneDispose()
        {
            _lateTickHandler.RemoveListener(this);
        }

    }
}