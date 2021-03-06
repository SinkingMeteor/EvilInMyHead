
namespace Sheldier.Common
{
    public class LateTickHandler : BaseTickHandler<ILateTickListener>
    {
        private void LateUpdate()
        {
            SetTickDelta();

            for (int i = 0; i < _tickListeners.Count; i++)
            {
                if(!_scheduledToRemove.Contains(_tickListeners[i]))
                    _tickListeners[i].LateTick();
            }
            
            if(_scheduledToRemove.Count == 0)
                return;

            for (int i = 0; i < _scheduledToRemove.Count; i++)
                _tickListeners.Remove(_scheduledToRemove[i]);
            _scheduledToRemove.Clear();
        }
    }
}