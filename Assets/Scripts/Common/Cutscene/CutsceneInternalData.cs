using Sheldier.Actors;
using Sheldier.Actors.AI;
using Sheldier.Actors.Data;
using Sheldier.Data;

namespace Sheldier.Common.Cutscene
{
    public class CutsceneInternalData
    {
        public SceneActorsDatabase SceneActorsDatabase => _sceneActorsDatabase;
        public Actor CurrentPlayer => _currentPlayer;
        public ActorsAIMoveModule ActorsAIMoveModule => _actorsAIMoveModule;
        public Database<ActorDynamicConfigData> DynamicConfigDatabase => _dynamicConfigDatabase;
        public DialoguesProvider DialoguesProvider => _dialoguesProvider;


        private readonly SceneActorsDatabase _sceneActorsDatabase;
        private readonly ActorsAIMoveModule _actorsAIMoveModule;
        private readonly Actor _currentPlayer;
        private readonly DialoguesProvider _dialoguesProvider;
        private readonly Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        public CutsceneInternalData(SceneActorsDatabase sceneActorsDatabase, ActorsAIMoveModule actorsAIMoveModule, Actor currentPlayer, DialoguesProvider dialoguesProvider,
            Database<ActorDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _currentPlayer = currentPlayer;
            _sceneActorsDatabase = sceneActorsDatabase;
            _actorsAIMoveModule = actorsAIMoveModule;
            _dialoguesProvider = dialoguesProvider;
        }

    }
}