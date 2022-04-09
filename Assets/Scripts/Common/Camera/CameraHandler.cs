using System.Collections;
using Sheldier.Common.Pause;
using Sheldier.Constants;
using UnityEngine;
using Zenject;

namespace Sheldier.Common
{
    public class CameraHandler : ILateTickListener, IPausable, ICameraFollower
    {
        public Camera CurrentSceneCamera => _camera.SceneCamera;

        private CameraSideMover _cameraSideMover;
        private CameraFollower _cameraFollower;
        
        private LateTickHandler _lateTickHandler;
        private IGameplayInputProvider _inputProvider;
        private PauseNotifier _pauseNotifier;
        
        private CameraPixelPerfect _pixelPerfectCameraTemplate;
        private CameraPixelPerfect _camera;

        public CameraHandler(LateTickHandler lateTickHandler, IGameplayInputProvider inputProvider, PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
            _inputProvider = inputProvider;
            _lateTickHandler = lateTickHandler;
        }
        
        public void InitializeOnScene()
        {
            _camera = GameObject.Instantiate(_pixelPerfectCameraTemplate);
            _camera.Initialize();
            _cameraFollower.SetCamera(_camera.SceneCamera);
            _cameraSideMover.SetCamera(_camera.SceneCamera);
            
            _lateTickHandler.AddListener(this);
            _pauseNotifier.Add(this);
        }
        
        public void Initialize()
        {
            _pixelPerfectCameraTemplate = Resources.Load<CameraPixelPerfect>(ResourcePaths.PIXEL_PERFECT_CAMERA);
            _cameraFollower = new CameraFollower();
            _cameraFollower.SetDependencies(_lateTickHandler);
            _cameraFollower.Initialize();
            
            _cameraSideMover = new CameraSideMover();
            _cameraSideMover.SetDependencies(_inputProvider, _cameraFollower, _lateTickHandler);
            
        }

        public void LateTick()
        {
            _camera.LateTick();
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
            _pauseNotifier.Remove(this);
        }

        public void Pause()
        {
           // _lateTickHandler.RemoveListener(this);
            _cameraSideMover.Pause();
        }

        public void Unpause()
        {
         //   _lateTickHandler.AddListener(this);
            _cameraSideMover.UnPause();
        }
    }
}