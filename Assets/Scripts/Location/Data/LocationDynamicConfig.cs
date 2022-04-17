using System;
using Sheldier.Data;

namespace Sheldier.GameLocation
{
    [Serializable]
    public class LocationDynamicConfig : IDatabaseItem
    {
        public string ID => TypeName;
        public string TypeName;
        public string VolumeProfile;

        public LocationDynamicConfig(LocationStaticConfig staticConfig)
        {
            TypeName = staticConfig.TypeName;
            VolumeProfile = staticConfig.VolumeProfile;
        }
    }
}