using Sheldier.Item;

namespace Sheldier.Common.Pool
{
    public class WeaponBlowPool : DefaultPool<WeaponBlow>
    {
        private TickHandler _tickHandler;

        public void SetDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        protected override void SetDependenciesToEntity(WeaponBlow entity)
        {
            entity.SetDependencies(_tickHandler);            
        }
    }
}