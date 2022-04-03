﻿using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Common.Animation;
using Sheldier.Common.Audio;
using Sheldier.Data;
using Sheldier.Item;
using Sheldier.Setup;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class DatabaseInstaller : Installer<DatabaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ActorStaticDataLoader>().AsSingle();
            Container.Bind<ItemStaticDataLoader>().AsSingle();
            Container.Bind<ActorDataFactory>().AsSingle();

            Container.Bind<AssetProvider<AudioUnit>>().To<AudioLoader>().AsSingle();
            Container.Bind<AssetProvider<Sprite>>().To<SpriteLoader>().AsSingle();
            Container.Bind<AssetProvider<AnimationData>>().To<AnimationLoader>().AsSingle();
            
            Container.Bind<Database<ActorStaticConfigData>>().To<ActorStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticBuildData>>().To<ActorStaticBuildDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticMovementData>>().To<ActorStaticMovementDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticDialogueData>>().To<ActorStaticDialogueDatabase>().AsSingle();
            
            Container.Bind<Database<ActorDynamicConfigData>>().To<ActorDynamicConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicMovementData>>().To<ActorDynamicMovementDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicDialogueData>>().To<ActorDynamicDialogueDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicEffectData>>().To<ActorDynamicEffectDatabase>().AsSingle();
            
            Container.Bind<Database<ItemStaticConfigData>>().To<ItemStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<ItemStaticWeaponData>>().To<ItemStaticWeaponDatabase>().AsSingle();
            Container.Bind<Database<ItemStaticProjectileData>>().To<ItemStaticProjectileDatabase>().AsSingle();
            
            Container.Bind<Database<ItemDynamicConfigData>>().To<ItemDynamicConfigDatabase>().AsSingle();
            Container.Bind<Database<ItemDynamicWeaponData>>().To<ItemDynamicWeaponDatabase>().AsSingle();
            
            
        }
    }
}