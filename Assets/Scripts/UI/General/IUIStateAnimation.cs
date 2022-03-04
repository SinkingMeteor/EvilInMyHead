using System.Threading.Tasks;

namespace Sheldier.UI
{
    public interface IUIStateAnimation
    {
        Task PlayAnimation();
        void Reset();
    }
}