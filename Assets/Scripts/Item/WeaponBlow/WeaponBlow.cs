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
        private IPoolSetter<WeaponBlow> _poolSetter;
        private AnimationLoader _animationLoader;

        public void Initialize(IPoolSetter<WeaponBlow> poolSetter)
        {
            _poolSetter = poolSetter;
            animator.Initialize();
        }

        public void SetDependencies(TickHandler tickHandler, AnimationLoader animationLoader)
        {
            _animationLoader = animationLoader;
            animator.SetDependencies(tickHandler);
        }
        public void OnInstantiated()
        {
        }
        public void SetAnimation(string data)
        {
            animator.Play(_animationLoader.Get(data, TextDataConstants.WEAPON_BLOW_ANIMATIONS_DIRECTORY));
            animator.OnAnimationEnd += SetToPool;
        }

        private void SetToPool()
        {
            animator.OnAnimationEnd -= SetToPool;
            _poolSetter.SetToPull(this);
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