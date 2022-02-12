namespace Sheldier.Common
{
    public interface IFixedTickListener
    {
        bool WantsToRemoveFromFixedTick { get; }
        void FixedTick();
        void OnTickDispose();
    }
}