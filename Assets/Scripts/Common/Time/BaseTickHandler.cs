using System.Collections.Generic;
using Sheldier.Common.Pause;
using UnityEngine;
using Zenject;

namespace Sheldier.Common
{
    public abstract class BaseTickHandler<T> : MonoBehaviour
    {
        public float TickDelta => _tickDelta;
        
        private List<T> _tickListeners = new List<T>();
        private List<T> _scheduledToRemove = new List<T>();
        private PauseNotifier _pauseNotifier;
        private float _tickDelta;
        
        [Inject]
        private void InjectDependencies(PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
        }
        public void AddListener(T tickListener) => _tickListeners.Add(tickListener);
        public void RemoveListener(T tickListener) => _scheduledToRemove.Add(tickListener);
        protected abstract void OnTick(T tickListener);
        private void Update()
        {
            SetTickDelta();

            for (int i = 0; i < _tickListeners.Count; i++)
            {
                if(!_scheduledToRemove.Contains(_tickListeners[i]))
                    OnTick(_tickListeners[i]);
            }
            
            if(_scheduledToRemove.Count == 0)
                return;

            for (int i = 0; i < _scheduledToRemove.Count; i++)
                _tickListeners.Remove(_scheduledToRemove[i]);
            _scheduledToRemove.Clear();
        }

        private void SetTickDelta()
        {
            _tickDelta = _pauseNotifier.IsPaused ? 0.0f : Time.deltaTime;
        }
    }
}