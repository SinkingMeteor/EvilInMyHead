
namespace Sheldier.Common
{
    public class FixedTickHandler : BaseTickHandler<IFixedTickListener>
    {
        private void FixedUpdate()
        {
            SetTickDelta();

            for (int i = 0; i < _tickListeners.Count; i++)
            {
                if(!_scheduledToRemove.Contains(_tickListeners[i]))
                    _tickListeners[i].FixedTick();
            }
            
            if(_scheduledToRemove.Count == 0)
                return;

            for (int i = 0; i < _scheduledToRemove.Count; i++)
                _tickListeners.Remove(_scheduledToRemove[i]);
            _scheduledToRemove.Clear();
        }
    }
}