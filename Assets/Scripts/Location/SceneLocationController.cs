using System.Threading.Tasks;
using Sheldier.Actors;
using Sheldier.Actors.Pathfinding;
using Sheldier.Common;
using Sheldier.Common.Asyncs;
using Sheldier.Constants;
using Sheldier.Item;
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

        public void SetDependencies(ItemSpawner itemSpawner, ActorSpawner actorSpawner, Pathfinder pathfinder)
        {
            _itemSpawner = itemSpawner;
            _actorSpawner = actorSpawner;
            _pathfinder = pathfinder;
        }

        public async Task LoadNewLocation(string locationName)
        {
            if (_currentLocation != null)
                await DisposeLocation();
            string path = ResourcePaths.LOCATIONS_PATH_TEMPLATE + locationName;
            var asyncHandle = await ResourceLoader.LoadAsync<Location>(path);
            _currentLocation = GameObject.Instantiate(asyncHandle);
            
            _itemSpawner.Initialize(_currentLocation.Placeholders);
            _actorSpawner.Initialize(_currentLocation.Placeholders);
            _currentLocation.Initialize();
            _pathfinder.InitializeOnNewLocation(_currentLocation.PathGrid);
        }

        public async Task DisposeLocation()
        {
            _actorSpawner.OnLocationDispose();
            _currentLocation.Dispose();
            
            var asyncHandle = Resources.UnloadUnusedAssets();
            await AsyncWaitersFactory.WaitUntil(() => asyncHandle.isDone);

        }
    }
}