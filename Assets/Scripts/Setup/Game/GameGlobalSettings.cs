namespace Sheldier.Setup
{
    public class GameGlobalSettings
    {
        private bool _isStarted;
        public bool IsStarted => _isStarted;
        public void SetStarted() => _isStarted = true;
    }
}