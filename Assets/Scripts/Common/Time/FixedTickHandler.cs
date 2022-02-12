using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common
{
    public class FixedTickHandler : MonoBehaviour
    {
        private List<IFixedTickListener> _tickListeners = new List<IFixedTickListener>();

        public void AddListener(IFixedTickListener tickListener) => _tickListeners.Add(tickListener);

        private void FixedUpdate()
        {
            for (int i = 0; i < _tickListeners.Count; i++)
            {
                if(!_tickListeners[i].WantsToRemoveFromFixedTick)
                {
                    _tickListeners[i].FixedTick();
                    continue;
                }
                int lastIndex = _tickListeners.Count - 1;
                (_tickListeners[i], _tickListeners[lastIndex]) = (_tickListeners[lastIndex], _tickListeners[i]);
                _tickListeners.RemoveAt(lastIndex);
                i -= 1;
            }
        }
    }
}