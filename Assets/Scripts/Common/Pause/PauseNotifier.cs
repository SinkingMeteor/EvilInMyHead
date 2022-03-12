using System.Collections.Generic;

namespace Sheldier.Common.Pause
{
    public class PauseNotifier
    {
        public bool IsPaused => _isPaused;
        
        private List<IPausable> _pauseListeners;
        private bool _isPaused;

        public void Initialize()
        {
            _pauseListeners = new List<IPausable>();
            _isPaused = false;
        }

        public void Add(IPausable pausable)
        {
            _pauseListeners.Add(pausable);
        }

        public void Remove(IPausable pausable)
        {
            _pauseListeners.Remove(pausable);
        }

        public void Pause()
        {
            for (int i = 0; i < _pauseListeners.Count; i++)
                _pauseListeners[i].Pause();
            _isPaused = true;
        }

        public void Unpause()
        {
            for (int i = 0; i < _pauseListeners.Count; i++)
                _pauseListeners[i].Unpause();
            _isPaused = false;
        }
        
        public void Clear()
        {
            if (_isPaused)
                Unpause();
            _pauseListeners.Clear();
        }
    }
}
