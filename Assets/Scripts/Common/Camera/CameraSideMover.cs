using System;
using System.Collections;
using UnityEngine;

namespace Sheldier.Common
{
    public class CameraSideMover
    {
        private IInputProvider _inputProvider;
        private Camera _camera;
        private CameraFollower _cameraFollower;
        private float _multiplier;
        private LateTickHandler _lateTickHandler;
        private Coroutine _multiplierCoroutine;

        private const float TOLERANCE = 0.05f;
        public void SetCamera(Camera camera)
        {
            _camera = camera;
            _multiplier = 1;
        }
        
        public void SetDependencies(IInputProvider inputProvider, CameraFollower cameraFollower,
            LateTickHandler lateTickHandler)
        {
            _lateTickHandler = lateTickHandler;
            _inputProvider = inputProvider;
            _cameraFollower = cameraFollower;
        }

        public void LateTick()
        {
            var cursorMousePosition = _inputProvider.CursorScreenCenterDirection * _multiplier;
           _camera.transform.position = _cameraFollower.TargetPosition + new Vector3(cursorMousePosition.x, cursorMousePosition.y, 0.0f);
        }

        public void Pause()
        {
            if(_multiplierCoroutine != null)
                _lateTickHandler.StopCoroutine(_multiplierCoroutine);
            _multiplier = 1.0f;
            _multiplierCoroutine = _lateTickHandler.StartCoroutine(SmoothSwitchMultiplierCoroutine(0));
        }

        public void UnPause()
        {
            if(_multiplierCoroutine != null)
                _lateTickHandler.StopCoroutine(_multiplierCoroutine);
            _multiplier = 0.0f;
            _multiplierCoroutine = _lateTickHandler.StartCoroutine(SmoothSwitchMultiplierCoroutine(1));
        }

        private IEnumerator SmoothSwitchMultiplierCoroutine(float to)
        {
            while (Math.Abs(_multiplier - to) > TOLERANCE)
            {
                _multiplier = Mathf.Lerp(_multiplier, to, Time.deltaTime * 5.0f);
                yield return null;
            }
        }
    }
}