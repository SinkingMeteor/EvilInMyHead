using System.Threading.Tasks;

namespace Sheldier.UI
{
    public interface IUIStateAnimation
    {
        void Initialize();
        Task PlayAnimation();
        void Reset();
    }
    public interface IUIStateAnimationAppearing : IUIStateAnimation{}
    public interface IUIStateAnimationDisappearing : IUIStateAnimation{}
}