using Sheldier.Actors.Builder;
using Sheldier.Actors.Data;
using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.GameLocation;
using Sheldier.Installers;

namespace Sheldier.Actors
{
    public class ActorSpawner
    {
        private readonly Database<ActorDynamicConfigData> _dynamicConfigDatabase;
        private readonly ScenePlayerController _scenePlayerController;
        private readonly SceneActorsDatabase _sceneActorsDatabase;
        private readonly IActorBuilder _actorBuilder;

        private LocationPlaceholdersKeeper _placeholdersKeeper;
        private ActorsInstaller _actorsInstaller;

        public ActorSpawner(IActorBuilder actorBuilder, SceneActorsDatabase sceneActorsDatabase, ScenePlayerController scenePlayerController, 
            Database<ActorDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _scenePlayerController = scenePlayerController;
            _sceneActorsDatabase = sceneActorsDatabase;
            _actorBuilder = actorBuilder;
        }
        public void Initialize(LocationPlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            LoadItems();
        }
        private void LoadItems()
        {
            int counter = 0;
            foreach (var placeholder in _placeholdersKeeper.ActorPlaceholders)
            {
                string typeName = null;

                if (placeholder.Reference.Reference == GameplayConstants.CURRENT_PLAYER)
                {
                    var controlledActorGuid = _scenePlayerController.ControlledActorGuid;
                    typeName = _dynamicConfigDatabase.Get(controlledActorGuid).TypeName;
                }
                else
                    typeName = placeholder.Reference.Reference;
                Actor actor = _actorBuilder.Build(typeName, placeholder.Guid);
                actor.name += counter++;
                actor.transform.position = placeholder.transform.position;
                _sceneActorsDatabase.Add(placeholder.Reference.Reference, actor);
                placeholder.Deactivate();
                
            }
        }
        public void OnLocationDispose()
        {
           _sceneActorsDatabase.Clear();
        }
    }
}