using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Zenject;

namespace Sheldier.Setup
{
    public class ActorStaticDataLoader
    {
        private Database<ActorStaticBuildData> _actorStaticBuildDatabase;
        private Database<ActorStaticConfigData> _actorStaticConfigDatabase;
        private Database<ActorStaticDialogueData> _actorStaticDialogueDatabase;
        private Database<ActorStaticMovementData> _actorStaticMovementDatabase;

        [Inject]
        private void InjectDependencies(Database<ActorStaticBuildData> actorStaticBuildDatabase, Database<ActorStaticConfigData> actorStaticConfigDatabase,
            Database<ActorStaticDialogueData> actorStaticDialogueDatabase, Database<ActorStaticMovementData> actorStaticMovementDatabase)
        {
            _actorStaticBuildDatabase = actorStaticBuildDatabase;
            _actorStaticConfigDatabase = actorStaticConfigDatabase;
            _actorStaticDialogueDatabase = actorStaticDialogueDatabase;
            _actorStaticMovementDatabase = actorStaticMovementDatabase;
        }
        
        public void LoadStaticData()
        {
            _actorStaticBuildDatabase.FillDatabase(TextDataConstants.ACTOR_BUILD_DATA);
            _actorStaticConfigDatabase.FillDatabase(TextDataConstants.ACTOR_CONFIG);
            _actorStaticDialogueDatabase.FillDatabase(TextDataConstants.ACTOR_DIALOGUE_DATA);
            _actorStaticMovementDatabase.FillDatabase(TextDataConstants.ACTOR_MOVEMENT_DATA);
        }
    }
}