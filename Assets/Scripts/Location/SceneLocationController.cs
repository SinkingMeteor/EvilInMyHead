using System;
using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Asyncs;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Item;
using UniRx;
using UnityEngine;
using Zenject;

namespace Sheldier.GameLocation
{
    public class SceneLocationController
    {
        public bool IsLocationExists => _currentLocation != null;

        private readonly Database<LocationDynamicConfig> _locationDynamicConfigDatabase;
        private readonly Database<LocationStaticConfig> _locationStaticConfigDatabase;
        private readonly PostProcessingController _postProcessingController;

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
                                       Database<LocationDynamicConfig> locationDynamicConfigDatabase)
        {
            _locationStaticConfigDatabase = locationStaticConfigDatabase;
            _locationDynamicConfigDatabase = locationDynamicConfigDatabase;
            _postProcessingController = postProcessingController;
            _itemSpawner = itemSpawner;
            _actorSpawner = actorSpawner;
            _pathfinder = pathfinder;
        }
        private async void OnReceivedLocationChangeRequest(ChangeLocationRequest request)
        {
            await LoadNewLocation(request.LocationReference);
        }
        
        public async Task LoadNewLocation(string locationName)
        {
            if (_currentLocation != null)
                await DisposeLocation();
            string path = ResourcePaths.LOCATIONS_PATH_TEMPLATE + locationName;
            var location = ResourceLoader.Load<Location>(path);
            _currentLocation = GameObject.Instantiate(location);

            string locationReference = _currentLocation.LocationReference.Reference;
            
            if (!_locationDynamicConfigDatabase.IsItemExists(locationReference))
                CreateNewLocationDynamicData(locationReference);
            
            _postProcessingController.ApplyPostProcessingProfile(_locationDynamicConfigDatabase.Get(locationReference).VolumeProfile);
            
            _locationChangeEvent = MessageBroker.Default.Receive<ChangeLocationRequest>().Subscribe(OnReceivedLocationChangeRequest);
            
            _itemSpawner.Initialize(_currentLocation.Placeholders);
            _actorSpawner.Initialize(_currentLocation.Placeholders);
            _currentLocation.Initialize();
            _pathfinder.InitializeOnNewLocation(_currentLocation.PathGrid);
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