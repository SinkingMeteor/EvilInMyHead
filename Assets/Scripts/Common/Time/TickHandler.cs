using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common
{
    public class TickHandler : MonoBehaviour
    {
        private List<ITickListener> _tickListeners = new List<ITickListener>();
        private List<ITickListener> _scheduledToRemove = new List<ITickListener>();
        public void AddListener(ITickListener tickListener) => _tickListeners.Add(tickListener);
        public void RemoveListener(ITickListener tickListener) => _scheduledToRemove.Add(tickListener);
        private void Update()
        {
            for (int i = 0; i < _tickListeners.Count; i++)
                _tickListeners[i].Tick();
            
            if(_scheduledToRemove.Count == 0)
                return;

            for (int i = 0; i < _scheduledToRemove.Count; i++)
                _tickListeners.Remove(_scheduledToRemove[i]);
            _scheduledToRemove.Clear();
        }
    }
}
