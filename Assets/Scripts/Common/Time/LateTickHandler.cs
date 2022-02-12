using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common
{
    public class LateTickHandler : MonoBehaviour
    {
        private List<ILateTickListener> _tickListeners = new List<ILateTickListener>();
        public void AddListener(ILateTickListener tickListener) => _tickListeners.Add(tickListener);
        
        private void LateUpdate()
        {
            for (int i = 0; i < _tickListeners.Count; i++)
            {
                if(!_tickListeners[i].WantsToRemoveFromLateTick)
                {
                    _tickListeners[i].LateTick();
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