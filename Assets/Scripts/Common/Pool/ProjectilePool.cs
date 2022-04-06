using Sheldier.Data;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class ProjectilePool : DefaultPool<Projectile>
    {
        private Database<ItemStaticProjectileData> _staticProjectileDatabase;
        private TickHandler _tickHandler;
        private AssetProvider<Sprite> _spriteLoader;
        
        public void SetDependencies(TickHandler tickHandler, Database<ItemStaticProjectileData> staticProjectileDatabase, AssetProvider<Sprite> spriteLoader)
        {
            _staticProjectileDatabase = staticProjectileDatabase;
            _spriteLoader = spriteLoader;
            _tickHandler = tickHandler;
        }
        protected override void SetDependenciesToEntity(Projectile entity)
        {
            entity.SetDependencies(_tickHandler, _staticProjectileDatabase, _spriteLoader);
        }
    }
}