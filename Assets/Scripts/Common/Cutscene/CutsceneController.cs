using Sheldier.Actors;
using Sheldier.Actors.Data;
using Sheldier.Data;
using Sheldier.GameLocation;
using Sheldier.Installers;
using UnityEngine;

namespace Sheldier.Common.Cutscene
{
    public class CutsceneController
    {
        private readonly Database<LocationDynamicConfig> _dynamicLocationDataDatabase;
        private readonly SceneLocationController _sceneLocationController;
        private readonly SceneActorsDatabase _sceneActorsDatabase;
        private readonly AssetProvider<Cutscene> _cutsceneLoader;
        private readonly GameplayInstaller _gameplayInstaller;

        private ScenePlayerController _scenePlayerController;

        public CutsceneController(SceneActorsDatabase sceneActorsDatabase,
                                  ScenePlayerController scenePlayerController,
                                  SceneLocationController sceneLocationController,
                                  Database<LocationDynamicConfig> dynamicLocationDataDatabase,
                                  AssetProvider<Cutscene> cutsceneLoader,
                                  GameplayInstaller gameplayInstaller)
        {
            _gameplayInstaller = gameplayInstaller;
            _cutsceneLoader = cutsceneLoader;
            _dynamicLocationDataDatabase = dynamicLocationDataDatabase;
            _sceneLocationController = sceneLocationController;
            _sceneActorsDatabase = sceneActorsDatabase;
            _scenePlayerController = scenePlayerController;
        }

        public void Initialize()
        {
            _sceneLocationController.OnNewLocationLoaded += OnLocationLoaded;
        }

        private void StartCutscene(string cutsceneName)
        {
            Cutscene cutscene = _cutsceneLoader.Get(cutsceneName);
            Cutscene cutsceneInstance = Object.Instantiate(cutscene); 
            if (ReferenceEquals(cutsceneInstance, null))
                return;
            Actor controlledActor = GetCurrentPlayer();
            controlledActor.LockInput();
            _gameplayInstaller.InjectGameObject(cutsceneInstance.gameObject);
            cutsceneInstance.Play(OnCutsceneComplete);
        }

        public void Dispose()
        {
            _sceneLocationController.OnNewLocationLoaded -= OnLocationLoaded;
        }

        private void OnLocationLoaded(string locationName)
        {
            return;
            if (!_dynamicLocationDataDatabase.IsItemExists(locationName))
                return;
            var locationData = _dynamicLocationDataDatabase.Get(locationName);
            if(locationData.OnLoadCutscene == null)
                return;
            var startCutsceneName = locationData.OnLoadCutscene;
            locationData.OnLoadCutscene = null;
            StartCutscene(startCutsceneName);
        }
        private void OnCutsceneComplete(Cutscene cutscene)
        {
            GetCurrentPlayer().UnlockInput();
            Object.Destroy(cutscene.gameObject);
        }

        private Actor GetCurrentPlayer()
        {
            var guid = _scenePlayerController.ControlledActorGuid;
            return _sceneActorsDatabase.Get(guid);
        } 
    }
    
}