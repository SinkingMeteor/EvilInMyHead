using System;
using System.Threading.Tasks;

namespace Sheldier.Setup
{
    public interface ILoadOperation
    {
        string LoadLabel { get; }
        Task Load(Action<float> SetProgress);
    }
}