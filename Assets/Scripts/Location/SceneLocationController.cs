using System;
using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Asyncs;
using Sheldier.Constants;
using Sheldier.Item;
using UniRx;
using UnityEngine;
using Zenject;

namespace Sheldier.GameLocation
{
    public class SceneLocationController
    {
        public bool IsLocationExists => _currentLocation != null;
        
        private Location _currentLocation;
        private ItemSpawner _itemSpawner;
        private ActorSpawner _actorSpawner;
        private Pathfinder _pathfinder;
        
        private IDisposable _locationChangeEvent;
        public SceneLocationController(ItemSpawner itemSpawner, ActorSpawner actorSpawner, Pathfinder pathfinder)
        {
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
            
            _locationChangeEvent = MessageBroker.Default.Receive<ChangeLocationRequest>().Subscribe(OnReceivedLocationChangeRequest);
            
            _itemSpawner.Initialize(_currentLocation.Placeholders);
            _actorSpawner.Initialize(_currentLocation.Placeholders);
            _currentLocation.Initialize();
            _pathfinder.InitializeOnNewLocation(_currentLocation.PathGrid);
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