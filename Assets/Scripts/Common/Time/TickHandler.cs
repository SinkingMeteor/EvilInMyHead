using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common
{
    public class TickHandler : MonoBehaviour
    {
        private List<ITickListener> _tickListeners = new List<ITickListener>();

        public void AddListener(ITickListener tickListener) => _tickListeners.Add(tickListener);

        private void Update()
        {
            for (int i = 0; i < _tickListeners.Count; i++)
            {
                if(_tickListeners[i].Tick())
                    continue;
                int lastIndex = _tickListeners.Count - 1;
                (_tickListeners[i], _tickListeners[lastIndex]) = (_tickListeners[lastIndex], _tickListeners[i]);
                _tickListeners.RemoveAt(lastIndex);
                i -= 1;
            }
        }

    }
}
