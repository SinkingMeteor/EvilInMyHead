using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common
{
    public class LateTickHandler : MonoBehaviour
    {
        private List<ILateTickListener> _tickListeners = new List<ILateTickListener>();
        private List<ILateTickListener> _scheduledToRemove = new List<ILateTickListener>();
        public void AddListener(ILateTickListener tickListener) => _tickListeners.Add(tickListener);
        public void RemoveListener(ILateTickListener tickListener) => _scheduledToRemove.Add(tickListener);
        private void Update()
        {
            for (int i = 0; i < _tickListeners.Count; i++)
                _tickListeners[i].LateTick();
            
            if(_scheduledToRemove.Count == 0)
                return;

            for (int i = 0; i < _scheduledToRemove.Count; i++)
                _tickListeners.Remove(_scheduledToRemove[i]);
            _tickListeners.Clear();
        }
    }
}