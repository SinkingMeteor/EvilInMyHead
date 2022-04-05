using Sheldier.Common.Animation;
using Sheldier.Data;
using Sheldier.Item;

namespace Sheldier.Common.Pool
{
    public class WeaponBlowPool : DefaultPool<WeaponBlow>
    {
        private TickHandler _tickHandler;
        private AssetProvider<AnimationData> _animationLoader;

        public void SetDependencies(TickHandler tickHandler, AssetProvider<AnimationData> animationLoader)
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