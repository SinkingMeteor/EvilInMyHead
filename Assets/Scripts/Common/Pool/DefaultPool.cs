using System.Collections.Generic;
using Sheldier.Common.Pause;
using Sheldier.Installers;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Pool
{
    public abstract class DefaultPool<T> : MonoBehaviour, IPoolSetter<T> where T : MonoBehaviour, IPoolObject<T>
    {
        [SerializeField] private Transform poolTransform;
        [SerializeField] private int startEntitiesAmount;
        [SerializeField] private T _entity;

        private Queue<T> _pooledEntities;
        public void Initialize()
        {
            _pooledEntities = new Queue<T>();
            FillPool();
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
            itemSlot.transform.localScale = Vector3.one;
            itemSlot.transform.rotation = Quaternion.Euler(0,0,0);
            itemSlot.Reset();
            itemSlot.gameObject.SetActive(false);
            _pooledEntities.Enqueue(itemSlot);

        }
        private T InstantiateEntity()
        {
            var entity = Instantiate(_entity, poolTransform, false);
            SetDependenciesToEntity(entity);
            entity.Initialize(this);
            entity.gameObject.SetActive(false);
            return entity;
        }
        protected abstract void SetDependenciesToEntity(T entity);
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
            foreach (var entity in _pooledEntities)
            {
                entity.Dispose();
                Destroy(entity.Transform.gameObject);
            }
            _pooledEntities.Clear();
        }

    }
}