using UnityEngine;

namespace Sheldier.Common.Pool
{
    public interface IPool<T> where T : MonoBehaviour
    {
        public void SetToPull(T itemSlot);
        public T GetFromPool();
    }
}