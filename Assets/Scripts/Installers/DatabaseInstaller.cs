using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Common.Audio;
using Sheldier.Common.Cutscene;
using Sheldier.Data;
using Sheldier.GameLocation;
using Sheldier.Graphs.DialogueSystem;
using Sheldier.Item;
using Sheldier.Setup;
using Sheldier.UI;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Sheldier.Installers
{
    public class DatabaseInstaller : Installer<DatabaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ActorStaticDataLoader>().AsSingle();
            Container.Bind<ItemStaticDataLoader>().AsSingle();
            Container.Bind<UIStaticDataLoader>().AsSingle();
            Container.Bind<StatsStaticDataLoader>().AsSingle();
            Container.Bind<LocationStaticDataLoader>().AsSingle();
            
            Container.Bind<ActorDataFactory>().AsSingle();

            Container.Bind<AssetProvider<AudioUnit>>().To<AudioLoader>().AsSingle();
            Container.Bind<AssetProvider<Sprite>>().To<SpriteLoader>().AsSingle();
            Container.Bind<AssetProvider<AnimationData>>().To<AnimationLoader>().AsSingle();
            Container.Bind<AssetProvider<ActorAnimationCollection>>().To<AnimationCollectionLoader>().AsSingle();
            Container.Bind<AssetProvider<TextAsset>>().To<DataLoader>().AsSingle();
            Container.Bind<AssetProvider<DialogueSystemGraph>>().To<DialoguesLoader>().AsSingle();
            Container.Bind<AssetProvider<VolumeProfile>>().To<PostProcessingLoader>().AsSingle();
            Container.Bind<AssetProvider<Cutscene>>().To<CutsceneLoader>().AsSingle();
            
            Container.Bind<Database<ActorStaticConfigData>>().To<ActorStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorStaticBuildData>>().To<ActorStaticBuildDatabase>().AsSingle();
            Container.Bind<Database<DialogueStaticData>>().To<DialogueStaticDatabase>().AsSingle();
            
            Container.Bind<Database<ActorDynamicConfigData>>().To<ActorDynamicConfigDatabase>().AsSingle();
            Container.Bind<Database<ActorDynamicEffectData>>().To<ActorDynamicEffectDatabase>().AsSingle();
            
            Container.Bind<Database<ItemStaticConfigData>>().To<ItemStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<ItemStaticWeaponData>>().To<ItemStaticWeaponDatabase>().AsSingle();
            Container.Bind<Database<ItemStaticProjectileData>>().To<ItemStaticProjectileDatabase>().AsSingle();
            Container.Bind<Database<ItemStaticInventorySlotData>>().To<ItemStaticInventorySlotDatabase>().AsSingle();
            
            Container.Bind<Database<ItemDynamicConfigData>>().To<ItemDynamicConfigDatabase>().AsSingle();
            Container.Bind<Database<ItemDynamicWeaponData>>().To<ItemDynamicWeaponDatabase>().AsSingle();

            Container.Bind<Database<UIPerformStaticData>>().To<UIPerformStaticDatabase>().AsSingle();

            Container.Bind<Database<StaticNumericalStatCollection>>().To<StaticNumericalStatDatabase>().AsSingle();
            Container.Bind<Database<StaticStringStatCollection>>().To<StaticStringStatDatabase>().AsSingle();
            Container.Bind<Database<DynamicNumericalEntityStatsCollection>>().To<DynamicGeneralNumericalStatsDatabase>().AsSingle();
            Container.Bind<Database<DynamicStringEntityStatsCollection>>().To<DynamicGeneralStringStatsDatabase>().AsSingle();
            
            Container.Bind<Database<LocationStaticConfig>>().To<LocationStaticConfigDatabase>().AsSingle();
            Container.Bind<Database<LocationDynamicConfig>>().To<LocationDynamicConfigDatabase>().AsSingle();
            Container.Bind<CurrentSceneDynamicData>().AsSingle();
            
            Container.Bind<SceneActorsDatabase>().AsSingle();
            Container.Bind<Database<SimpleItem>>().To<SimpleItemDatabase>().AsSingle();

        }
    }
}