namespace Sheldier.Setup
{
    public class GameGlobalSettings
    {
        private static bool _isStarted;
        public static bool IsStarted => _isStarted;
        public void SetStarted() => _isStarted = true;
    }
}