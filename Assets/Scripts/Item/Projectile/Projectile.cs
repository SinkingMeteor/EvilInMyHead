using System.Collections;
using Sheldier.Common;
using Sheldier.Common.Pool;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;
using Zenject;

namespace Sheldier.Item
{
    public class Projectile : MonoBehaviour, IPoolObject<Projectile>, ITickListener
    {
        private Vector2 _movementDirection;
        public Transform Transform => transform;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private Database<ItemStaticProjectileData> _staticProjectileData;
        private ItemStaticProjectileData _config;
        private Coroutine _projectileLifetime;
        private IPoolSetter<Projectile> _poolSetter;
        private TickHandler _tickHandler;
        private SpriteLoader _spriteLoader;
        private bool _isPaused;

        public void Initialize(IPoolSetter<Projectile> poolSetter)
        {
            _poolSetter = poolSetter;
        }
        public void SetDependencies(TickHandler tickHandler, Database<ItemStaticProjectileData> staticProjectileData, SpriteLoader spriteLoader)
        {
            _spriteLoader = spriteLoader;
            _staticProjectileData = staticProjectileData;
            _tickHandler = tickHandler;
        }
        public void OnInstantiated()
        {
            _tickHandler.AddListener(this);
        }
        public void SetDirection(Vector2 dir)
        {
            _movementDirection = dir;
        }
        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        public void SetData(string typeName)
        {
            _config = _staticProjectileData.Get(typeName);
            _projectileLifetime = StartCoroutine(StartLifetimeCoroutine());
            spriteRenderer.sprite = _spriteLoader.Get(_config.Icon, TextDataConstants.PROJECTILES_ICONS_DIRECTORY);
        }

        public void Tick()
        {
            Vector3 position = transform.position;
            transform.position = Vector3.Lerp(position, position + new Vector3(_movementDirection.x, _movementDirection.y, 0.0f) * _config.Speed, Time.deltaTime * 3f);
        }

        public void Reset()
        {
            if(_projectileLifetime != null)
                StopCoroutine(_projectileLifetime);
            _tickHandler.RemoveListener(this);
        }
        
        public void Dispose()
        {
            
        }
        private IEnumerator StartLifetimeCoroutine()
        {
            float totalTime = _config.Duration;

            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                yield return null;
            }
            _poolSetter.SetToPull(this);
        }


    }
}
