using Sheldier.Common;

namespace Sheldier.Actors
{
    public class ActorInputController
    {
        public IInputProvider CurrentInputProvider => _currentInputProvider;

        private IInputProvider _currentInputProvider;
        private IInputProvider _nullProvider;
        private IInputProvider _realInputProvider;
        

        public void Initialize()
        {
            _nullProvider = new NullInputProvider();
            _currentInputProvider = _nullProvider;
        }

        public void SetInputProvider(IInputProvider inputProvider)
        {
            _realInputProvider = inputProvider;
            _currentInputProvider = _realInputProvider;
        }
        public void LockInput()
        {
            _currentInputProvider = _nullProvider;
        }

        public void UnlockInput()
        {
            _currentInputProvider = _realInputProvider;
        }
    }
}