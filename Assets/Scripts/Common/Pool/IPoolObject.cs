using Sheldier.Common.Pause;
using UnityEngine;

namespace Sheldier.Common.Pool
{
    public interface IPoolObject<T> : ITransformable, IResetable, ITickListener where T : MonoBehaviour
    {
        void Initialize(IPoolSetter<T> poolSetter);

        void OnInstantiated();
    }
}