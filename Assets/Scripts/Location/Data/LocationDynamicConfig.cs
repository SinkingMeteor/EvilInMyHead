using System;
using Sheldier.Data;

namespace Sheldier.GameLocation
{
    [Serializable]
    public class LocationDynamicConfig : Database<EntityPositionDynamicData>, IDatabaseItem 
    {
        public string ID => TypeName;
        public string TypeName;
        public string VolumeProfile;
        public string OnLoadCutscene;

        public LocationDynamicConfig(LocationStaticConfig staticConfig)
        {
            TypeName = staticConfig.TypeName;
            VolumeProfile = staticConfig.VolumeProfile;
            OnLoadCutscene = staticConfig.OnLoadCutscene;
        }
    }
}