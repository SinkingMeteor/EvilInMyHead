using Sheldier.Common;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.GameLocation;
using UnityEngine;

namespace Sheldier.Setup
{
    public class LocationStaticDataLoader
    {
        private readonly AssetProvider<TextAsset> _dataLoader;
        private readonly Database<LocationStaticConfig> _locationStaticConfigDatabase;

        public LocationStaticDataLoader(Database<LocationStaticConfig> locationStaticConfigDatabase, AssetProvider<TextAsset> dataLoader)
        {
            _dataLoader = dataLoader;
            _locationStaticConfigDatabase = locationStaticConfigDatabase;
        }

        public void LoadStaticData()
        {
            _locationStaticConfigDatabase.FillDatabase(_dataLoader.Get(AssetPathConstants.LOCATION_CONFIG));
        }
    }
}