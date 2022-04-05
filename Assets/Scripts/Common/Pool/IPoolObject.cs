using Sheldier.Common.Pause;
using UnityEngine;

namespace Sheldier.Common.Pool
{
    public interface IPoolObject<T> : ITransformable, IResetable where T : MonoBehaviour
    {
        void Initialize(IPool<T> pool);

        void OnInstantiated();

        void Dispose();
    }
}