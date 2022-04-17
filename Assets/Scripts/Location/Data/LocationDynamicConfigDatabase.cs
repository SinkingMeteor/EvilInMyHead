using Sheldier.Common.SaveSystem;
using Sheldier.Data;

namespace Sheldier.GameLocation
{
    public class LocationDynamicConfigDatabase : Database<LocationDynamicConfig>
    {
        public LocationDynamicConfigDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
    }
}