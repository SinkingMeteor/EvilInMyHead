using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Asyncs;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Item;
using Sheldier.UI;
using UniRx;
using UnityEngine;

namespace Sheldier.GameLocation
{
    public class SceneLocationController
    {
        public event Action<string> OnNewLocationLoaded;
        
        public bool IsLocationExists => _currentLocation != null;

        private readonly Database<LocationDynamicConfig> _locationDynamicConfigDatabase;
        private readonly Database<LocationStaticConfig> _locationStaticConfigDatabase;
        private readonly PostProcessingController _postProcessingController;
        private readonly CurrentSceneDynamicData _currentSceneDynamicData;
        private readonly PersistUI _persistUI;

        private Location _currentLocation;
        private ItemSpawner _itemSpawner;
        private ActorSpawner _actorSpawner;
        private Pathfinder _pathfinder;

        private IDisposable _locationChangeEvent;

        public SceneLocationController(ItemSpawner itemSpawner,
                                       ActorSpawner actorSpawner, 
                                       Pathfinder pathfinder,
                                       PostProcessingController postProcessingController,
                                       Database<LocationStaticConfig> locationStaticConfigDatabase,
                                       Database<LocationDynamicConfig> locationDynamicConfigDatabase,
                                       CurrentSceneDynamicData currentSceneDynamicData,
                                       PersistUI persistUI)
        {
            _persistUI = persistUI;
            _currentSceneDynamicData = currentSceneDynamicData;
            _locationStaticConfigDatabase = locationStaticConfigDatabase;
            _locationDynamicConfigDatabase = locationDynamicConfigDatabase;
            _postProcessingController = postProcessingController;
            _itemSpawner = itemSpawner;
            _actorSpawner = actorSpawner;
            _pathfinder = pathfinder;
        }
        private async void OnReceivedLocationChangeRequest(ChangeLocationRequest request)
        {
            await _persistUI.Fader.FadeIn();
            await LoadNewLocation(request.LocationReference);
            await _persistUI.Fader.FadeOut();
        }
        
        public async Task LoadNewLocation(string locationName)
        {
            if (_currentLocation != null)
                await DisposeLocation();
            string path = ResourcePaths.LOCATIONS_PATH_TEMPLATE + locationName;
            var location = ResourceLoader.Load<Location>(path);
            _currentLocation = GameObject.Instantiate(location);

            string locationReference = _currentLocation.LocationReference.Reference;
            _currentSceneDynamicData.SetCurrentScene(locationReference);
            
            if (!_locationDynamicConfigDatabase.IsItemExists(locationReference))
                CreateNewLocationDynamicData(locationReference);

            string volumeProfile = _locationDynamicConfigDatabase.Get(locationReference).VolumeProfile;
            if(volumeProfile != "None")
                _postProcessingController.ApplyPostProcessingProfile(volumeProfile);
            
            _locationChangeEvent = MessageBroker.Default.Receive<ChangeLocationRequest>().Subscribe(OnReceivedLocationChangeRequest);
            
            _itemSpawner.Initialize(_currentLocation.Placeholders);
            _actorSpawner.Initialize(_currentLocation.Placeholders);
            _currentLocation.Initialize();
            _pathfinder.InitializeOnNewLocation(_currentLocation.PathGrid);
            
            OnNewLocationLoaded?.Invoke(locationReference);
        }

        private void CreateNewLocationDynamicData(string locationReference)
        {
            var staticConfig = _locationStaticConfigDatabase.Get(locationReference);
            _locationDynamicConfigDatabase.Add(new LocationDynamicConfig(staticConfig));
        }

        public async Task DisposeLocation()
        {
            _actorSpawner.OnLocationDispose();
            _currentLocation.Dispose();
            _locationChangeEvent.Dispose();
            GameObject.Destroy(_currentLocation.gameObject);
            var asyncHandle = Resources.UnloadUnusedAssets();
            await AsyncWaitersFactory.WaitUntil(() => asyncHandle.isDone);
        }
    }
    
}