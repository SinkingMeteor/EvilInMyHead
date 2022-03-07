namespace Sheldier.UI
{
    public interface IUIElement
    {
        void Initialize();
        void OnActivated();
        void OnDeactivated();
        void Tick();

        void Dispose();
    }
}