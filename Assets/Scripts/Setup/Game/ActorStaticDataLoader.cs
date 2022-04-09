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
        private Database<ActorStaticDialogueData> _actorStaticDialogueDatabase;
        private Database<ActorStaticMovementData> _actorStaticMovementDatabase;
        private AssetProvider<TextAsset> _dataLoader;

        [Inject]
        private void InjectDependencies(Database<ActorStaticBuildData> actorStaticBuildDatabase, Database<ActorStaticConfigData> actorStaticConfigDatabase,
            Database<ActorStaticDialogueData> actorStaticDialogueDatabase, Database<ActorStaticMovementData> actorStaticMovementDatabase, AssetProvider<TextAsset> dataLoader)
        {
            _dataLoader = dataLoader;
            _actorStaticBuildDatabase = actorStaticBuildDatabase;
            _actorStaticConfigDatabase = actorStaticConfigDatabase;
            _actorStaticDialogueDatabase = actorStaticDialogueDatabase;
            _actorStaticMovementDatabase = actorStaticMovementDatabase;
        }
        
        public void LoadStaticData()
        {
            _actorStaticBuildDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ACTOR_BUILD_DATA));
            _actorStaticConfigDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ACTOR_CONFIG));
            _actorStaticDialogueDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ACTOR_DIALOGUE_DATA));
            _actorStaticMovementDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.ACTOR_MOVEMENT_DATA));
        }
    }
}