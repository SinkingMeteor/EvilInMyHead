using UnityEngine;

namespace Sheldier.Common.Pool
{
    public interface IPoolSetter<T> where T : MonoBehaviour
    {
        public void SetToPull(T itemSlot);
    }
}