﻿using Sheldier.Data;
using Sheldier.Item;

namespace Sheldier.Common.Pool
{
    public class ProjectilePool : DefaultPool<Projectile>
    {
        private Database<ItemStaticProjectileData> _staticProjectileDatabase;
        private TickHandler _tickHandler;
        private SpriteLoader _spriteLoader;

        public void SetDependencies(TickHandler tickHandler, Database<ItemStaticProjectileData> staticProjectileDatabase, SpriteLoader spriteLoader)
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