namespace Sheldier.Common
{
    public interface ILateTickListener
    {
        bool WantsToRemoveFromLateTick { get; }
        void LateTick();
        void OnTickDispose();
    }
}