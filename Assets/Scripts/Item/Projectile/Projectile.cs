using System.Collections;
using Sheldier.Common;
using Sheldier.Common.Pool;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Item
{
    public class Projectile : MonoBehaviour, IPoolObject<Projectile>
    {
        private Vector2 _movementDirection;
        public Transform Transform => transform;

        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private ProjectileConfig _config;
        private Coroutine _projectileLifetime;
        private IPoolSetter<Projectile> _poolSetter;
        private TickHandler _tickHandler;

        public void Initialize(IPoolSetter<Projectile> poolSetter, TickHandler tickHandler)
        {
            _poolSetter = poolSetter;
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
        public void SetConfig(ProjectileConfig config)
        {
            _config = config;
            _projectileLifetime = StartCoroutine(StartLifetimeCoroutine());
            spriteRenderer.sprite = _config.Icon;
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
        }
        private IEnumerator StartLifetimeCoroutine()
        {
            float totalTime = _config.Duration;

            while (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                yield return null;
            }
            _tickHandler.RemoveListener(this);
            _poolSetter.SetToPull(this);
        }


    }
}
