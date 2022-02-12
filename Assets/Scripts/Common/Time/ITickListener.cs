namespace Sheldier.Common
{
    public interface ITickListener
    {
        bool WantsToRemoveFromTick { get; }
        void Tick();
        void OnTickDispose();
    }
}