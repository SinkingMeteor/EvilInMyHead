using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class DefaultPool<T> : MonoBehaviour, ITickListener, IPoolSetter<T> where T : MonoBehaviour, IPoolObject<T>
    {
        [SerializeField] private Transform poolTransform;
        [SerializeField] private int startEntitiesAmount;
        [SerializeField] private T _entity;

        private Queue<T> _pooledEntities;
        private List<T> _activeEntities;
        private TickHandler _tickHandler;

        public void Initialize()
        {
            _pooledEntities = new Queue<T>();
            _activeEntities = new List<T>();
            FillPool();
            _tickHandler.AddListener(this);
        }

        [Inject]
        private void InjectDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        public T GetFromPool()
        {
            if (_pooledEntities.Count > 0)
            {
                var poolObj = _pooledEntities.Dequeue();
                _activeEntities.Add(poolObj);
                poolObj.gameObject.SetActive(true);
                return poolObj;
            }
            var instantiatedObj = InstantiateEntity();
            _activeEntities.Add(instantiatedObj);
            instantiatedObj.gameObject.SetActive(true);
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
            _tickHandler.RemoveListener(this);
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

        public void Tick()
        {
            for (int i = 0; i < _activeEntities.Count; i++)
            {
                if (_pooledEntities.Contains(_activeEntities[i]))
                {
                    int lastIndex = _activeEntities.Count - 1;
                    (_activeEntities[lastIndex], _activeEntities[i]) = (_activeEntities[i], _activeEntities[lastIndex]);
                    _activeEntities.RemoveAt(lastIndex);
                    i -= 1;
                    continue;
                }
                _activeEntities[i].Tick();
            }
        }

    }
}