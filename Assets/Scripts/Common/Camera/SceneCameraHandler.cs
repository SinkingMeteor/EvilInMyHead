using System;
using UnityEngine;
using Zenject;

namespace Sheldier.Common
{
    public class SceneCameraHandler : MonoBehaviour, ITickListener, ILateTickListener
    {
        public Camera CurrentSceneCamera => _camera;
        public bool WantsToRemoveFromTick => _wantsToRemoveFromTick;
        public bool WantsToRemoveFromLateTick => _wantsToRemoveFromLateTick;
        
        [SerializeField] private CameraBordersConstrains _cameraBordersConstrains;

        private bool _wantsToRemoveFromTick;
        private bool _wantsToRemoveFromLateTick;
        private Camera _camera;

        private CameraSideMover _cameraSideMover;
        private CameraFollower _cameraFollower;
        
        private TickHandler _tickHandler;
        private LateTickHandler _lateTickHandler;
        public void Initialize(IInputProvider inputProvider)
        {
            _camera = Camera.main;

            _cameraFollower = new CameraFollower();
            _cameraFollower.SetDependencies(_camera);
            
            _cameraSideMover = new CameraSideMover();
            _cameraSideMover.SetDependencies(_camera, inputProvider, _cameraFollower);
            
            _cameraBordersConstrains.SetDependencies(_camera);
            
            _tickHandler.AddListener(this);
            _lateTickHandler.AddListener(this);
        }
        [Inject]
        private void InjectDependencies(LateTickHandler lateTickHandler, TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
            _lateTickHandler = lateTickHandler;
        }

        public void Tick()
        {            
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
            OnTickDispose();
        }

        public void OnTickDispose()
        {
            _wantsToRemoveFromTick = true;
            _wantsToRemoveFromLateTick = true;
        }
    }
}