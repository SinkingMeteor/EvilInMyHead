using System.Collections.Generic;
using Sheldier.Common.Pause;
using Sheldier.Installers;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class DefaultPool<T> : MonoBehaviour, IPoolSetter<T> where T : MonoBehaviour, IPoolObject<T>
    {
        [SerializeField] private Transform poolTransform;
        [SerializeField] private int startEntitiesAmount;
        [SerializeField] private T _entity;

        private Queue<T> _pooledEntities;
        private PoolInstaller _poolInstaller;

        public void Initialize()
        {
            _pooledEntities = new Queue<T>();
            FillPool();
        }

        [Inject]
        private void InjectDependencies(PoolInstaller poolInstaller)
        {
            _poolInstaller = poolInstaller;
        }
        public T GetFromPool()
        {
            if (_pooledEntities.Count > 0)
            {
                var poolObj = _pooledEntities.Dequeue();
                poolObj.gameObject.SetActive(true);
                poolObj.OnInstantiated();
                return poolObj;
            }
            var instantiatedObj = InstantiateEntity();
            instantiatedObj.gameObject.SetActive(true);
            instantiatedObj.OnInstantiated();
            return instantiatedObj;
        }
        public void SetToPull(T itemSlot)
        {
            itemSlot.Transform.SetParent(poolTransform, false);
            itemSlot.Reset();
            itemSlot.gameObject.SetActive(false);
            _pooledEntities.Enqueue(itemSlot);

        }
        private T InstantiateEntity()
        {
            var entity = Instantiate(_entity, poolTransform, false);
            _poolInstaller.InjectPoolObject(entity);
            entity.Initialize(this);
            entity.gameObject.SetActive(false);
            return entity;
        }

        private void FillPool()
        {
            for (var i = 0; i < startEntitiesAmount; i++)
            {
                _pooledEntities.Enqueue(InstantiateEntity());
            }
        }

        private void OnDestroy()
        {
            ResetPool();
        }

        private void ResetPool()
        {
            foreach (var t in _pooledEntities)
            {
                Destroy(t.Transform.gameObject);
            }
            _pooledEntities.Clear();
        }

    }
}