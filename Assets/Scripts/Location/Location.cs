using Sheldier.Actors.Pathfinding;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.GameLocation
{
    public class Location : MonoBehaviour
    {
        public LocationPlaceholdersKeeper Placeholders => placeholdersKeeper;
        public PathGrid PathGrid => pathGrid;
        
        [SerializeField] private LocationPlaceholdersKeeper placeholdersKeeper;
        [SerializeField] private PathGrid pathGrid;

        public void Initialize()
        {
            pathGrid.Initialize();
        }

        public void Dispose()
        {
            pathGrid.Dispose();
        }
    }
}
