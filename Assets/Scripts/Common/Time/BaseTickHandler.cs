using System.Collections.Generic;
using Sheldier.Common.Pause;
using UnityEngine;
using Zenject;

namespace Sheldier.Common
{
    public abstract class BaseTickHandler<T> : MonoBehaviour
    {
        public float TickDelta => _tickDelta;
        
        protected List<T> _tickListeners = new List<T>();
        protected List<T> _scheduledToRemove = new List<T>();
        private PauseNotifier _pauseNotifier;
        private float _tickDelta;
        
        [Inject]
        private void InjectDependencies(PauseNotifier pauseNotifier)
        {
            _pauseNotifier = pauseNotifier;
        }
        public void AddListener(T tickListener) => _tickListeners.Add(tickListener);
        public void RemoveListener(T tickListener) => _scheduledToRemove.Add(tickListener);
        protected void SetTickDelta()
        {
            _tickDelta = _pauseNotifier.IsPaused ? 0.0f : Time.deltaTime;
        }
    }
}