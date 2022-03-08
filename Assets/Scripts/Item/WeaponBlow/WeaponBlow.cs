using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Pause;
using Sheldier.Common.Pool;
using UnityEngine;
using Zenject;

namespace Sheldier.Item
{
    public class WeaponBlow : MonoBehaviour, IPoolObject<WeaponBlow>
    {
        public Transform Transform => transform;
        
        [SerializeField] private SimpleAnimator animator;
        private IPoolSetter<WeaponBlow> _poolSetter;

        public void Initialize(IPoolSetter<WeaponBlow> poolSetter)
        {
            _poolSetter = poolSetter;
            animator.Initialize();
        }
        public void OnInstantiated()
        {
        }
        
        public void SetAnimation(AnimationData data)
        {
            animator.Play(data);
            animator.OnAnimationEnd += SetToPool;
        }

        private void SetToPool()
        {
            animator.OnAnimationEnd -= SetToPool;
            _poolSetter.SetToPull(this);
        }

        public void Tick()
        {
        }

        public void Reset()
        {
            animator.Reset();
        }
    }
}