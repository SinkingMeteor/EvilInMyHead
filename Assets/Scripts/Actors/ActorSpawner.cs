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
        private readonly ICameraFollower _cameraFollower;
        private readonly IActorBuilder _actorBuilder;

        private LocationPlaceholdersKeeper _placeholdersKeeper;
        private ActorsInstaller _actorsInstaller;

        public ActorSpawner(IActorBuilder actorBuilder, SceneActorsDatabase sceneActorsDatabase, ScenePlayerController scenePlayerController, 
            Database<ActorDynamicConfigData> dynamicConfigDatabase, ICameraFollower cameraFollower)
        {
            _cameraFollower = cameraFollower;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _scenePlayerController = scenePlayerController;
            _sceneActorsDatabase = sceneActorsDatabase;
            _actorBuilder = actorBuilder;
        }
        public void Initialize(LocationPlaceholdersKeeper placeholdersKeeper)
        {
            _placeholdersKeeper = placeholdersKeeper;
            LoadActors();
        }
        private void LoadActors()
        {
            int counter = 0;    
            string typeName = null;
            bool isPlayer;
            foreach (var placeholder in _placeholdersKeeper.ActorPlaceholders)
            {
                isPlayer = false;
                if (placeholder.Reference.Reference == GameplayConstants.CURRENT_PLAYER)
                {
                    typeName = GetCurrentPlayer();
                    isPlayer = true;
                }
                else
                    typeName = placeholder.Reference.Reference;
                Actor actor = _actorBuilder.Build(typeName, placeholder.Guid, placeholder);
                actor.name += typeName;
                _sceneActorsDatabase.Add(typeName, actor);
                placeholder.Deactivate();
                
                if (isPlayer)
                {
                    _scenePlayerController.SetControl(actor);
                    _cameraFollower.SetFollowTarget(actor.transform);
                }
                
            }
        }

        private string GetCurrentPlayer()
        {
            var controlledActorGuid = _scenePlayerController.ControlledActorGuid;

            if (controlledActorGuid == null)
                return "Tom";
            
            var typeName = _dynamicConfigDatabase.Get(controlledActorGuid).TypeName;
            return typeName;
        }

        public void OnLocationDispose()
        {
           _sceneActorsDatabase.Clear();
        }
    }
}