using System.Threading.Tasks;

namespace Sheldier.UI
{
    public interface IUIStateAnimation
    {
        bool IsLocal { get; }
        void Initialize();
        Task PlayAnimation();
        void Reset();
    }
    public interface IUIStateAnimationAppearing : IUIStateAnimation{}
    public interface IUIStateAnimationDisappearing : IUIStateAnimation{}
}