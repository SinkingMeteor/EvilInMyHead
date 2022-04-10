using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;
using Zenject;

namespace Sheldier.Setup
{
    public class ActorStaticDataLoader
    {
        private Database<ActorStaticBuildData> _actorStaticBuildDatabase;
        private Database<ActorStaticConfigData> _actorStaticConfigDatabase;
        private AssetProvider<TextAsset> _dataLoader;

        [Inject]
        private void InjectDependencies(Database<ActorStaticBuildData> actorStaticBuildDatabase, Database<ActorStaticConfigData> actorStaticConfigDatabase,
            AssetProvider<TextAsset> dataLoader)
        {
            _dataLoader = dataLoader;
            _actorStaticBuildDatabase = actorStaticBuildDatabase;
            _actorStaticConfigDatabase = actorStaticConfigDatabase;
        }
        
        public void LoadStaticData()
        {
            _actorStaticBuildDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ACTOR_BUILD_DATA));
            _actorStaticConfigDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ACTOR_CONFIG));
        }
    }
}