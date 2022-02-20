using UnityEngine;

namespace Sheldier.Common.Pool
{
    public interface IPoolObject<T> : ITransformable, IResetable, ITickable where T : MonoBehaviour
    {
        void Initialize(IPoolSetter<T> poolSetter);
    }
}