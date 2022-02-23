using System;
using Sheldier.Setup;
using UnityEngine;
using Zenject;

namespace Sheldier.Common
{
    public class SceneCameraHandler : MonoBehaviour, ILateTickListener
    {
        public Camera CurrentSceneCamera => _camera;
        
        [SerializeField] private CameraBordersConstrains _cameraBordersConstrains;

        private Camera _camera;

        private CameraSideMover _cameraSideMover;
        private CameraFollower _cameraFollower;
        
        private LateTickHandler _lateTickHandler;
        private IInputProvider _inputProvider;

        public void Initialize()
        {
            _camera = Camera.main;

            _cameraFollower = new CameraFollower();
            _cameraFollower.SetDependencies(_camera);
            
            _cameraSideMover = new CameraSideMover();
            _cameraSideMover.SetDependencies(_camera, _inputProvider, _cameraFollower);
            
            _cameraBordersConstrains.SetDependencies(_camera);
            
            _lateTickHandler.AddListener(this);
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
            _cameraBordersConstrains.LateTick();
        }

        public void SetFollowTarget(Transform transform)
        {
            _cameraFollower.SetNewTarget(transform);
        }
        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _lateTickHandler.RemoveListener(this);
        }

    }
}