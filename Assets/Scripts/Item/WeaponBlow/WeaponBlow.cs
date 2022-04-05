using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;
using Zenject;

namespace Sheldier.Item
{
    public class WeaponBlow : MonoBehaviour, IPoolObject<WeaponBlow>
    {
        public Transform Transform => transform;
        
        [SerializeField] private SimpleAnimator animator;
        private IPool<WeaponBlow> _pool;
        private AssetProvider<AnimationData> _animationLoader;

        public void Initialize(IPool<WeaponBlow> pool)
        {
            _pool = pool;
            animator.Initialize();
        }

        public void SetDependencies(TickHandler tickHandler, AssetProvider<AnimationData> animationLoader)
        {
            _animationLoader = animationLoader;
            animator.SetDependencies(tickHandler);
        }
        public void OnInstantiated()
        {
        }
        public void SetAnimation(string data)
        {
            animator.Play(_animationLoader.Get(data));
            animator.OnAnimationEnd += SetToPool;
        }

        private void SetToPool()
        {
            animator.OnAnimationEnd -= SetToPool;
            _pool.SetToPull(this);
        }

        public void Reset()
        {
            animator.Reset();
        }
        public void Dispose()
        {
            
        }
    }
}