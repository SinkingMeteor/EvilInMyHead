using Sheldier.Data;
using Sheldier.Item;

namespace Sheldier.Common.Pool
{
    public class WeaponBlowPool : DefaultPool<WeaponBlow>
    {
        private TickHandler _tickHandler;
        private AnimationLoader _animationLoader;

        public void SetDependencies(TickHandler tickHandler, AnimationLoader animationLoader)
        {
            _animationLoader = animationLoader;
            _tickHandler = tickHandler;
        }
        protected override void SetDependenciesToEntity(WeaponBlow entity)
        {
            entity.SetDependencies(_tickHandler, _animationLoader);            
        }
    }
}