namespace Sheldier.UI
{
    public interface IVisualUIElement
    {
        void Initialize();
        void Activate();
        void Deactivate();
        void Tick();

        void Dispose();
    }
}