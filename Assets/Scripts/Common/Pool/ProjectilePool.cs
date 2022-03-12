using Sheldier.Item;

namespace Sheldier.Common.Pool
{
    public class ProjectilePool : DefaultPool<Projectile>
    {
        private TickHandler _tickHandler;

        public void SetDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        protected override void SetDependenciesToEntity(Projectile entity)
        {
            entity.SetDependencies(_tickHandler);
        }
    }
}