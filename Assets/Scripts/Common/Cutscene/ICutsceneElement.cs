using System.Collections;
using System.Threading.Tasks;

namespace Sheldier.Common.Cutscene
{
    public interface ICutsceneElement
    {
        bool WaitElement { get; }
        Task PlayCutScene();
    }
}