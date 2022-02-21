using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common
{
    public class FixedTickHandler : MonoBehaviour
    {
        private List<IFixedTickListener> _tickListeners = new List<IFixedTickListener>();
        private List<IFixedTickListener> _scheduledToRemove = new List<IFixedTickListener>();
        public void AddListener(IFixedTickListener tickListener) => _tickListeners.Add(tickListener);
        public void RemoveListener(IFixedTickListener tickListener) => _scheduledToRemove.Add(tickListener);
        private void Update()
        {
            for (int i = 0; i < _tickListeners.Count; i++)
                _tickListeners[i].FixedTick();
            
            if(_scheduledToRemove.Count == 0)
                return;

            for (int i = 0; i < _scheduledToRemove.Count; i++)
                _tickListeners.Remove(_scheduledToRemove[i]);
            _scheduledToRemove.Clear();
        }
    }
}