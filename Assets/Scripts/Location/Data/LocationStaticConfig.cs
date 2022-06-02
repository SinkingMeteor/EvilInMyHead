using System;
using Sheldier.Data;

namespace Sheldier.GameLocation
{
    [Serializable]
    public struct LocationStaticConfig : IDatabaseItem
    {
        public string ID => TypeName;
        public string TypeName;
        public string VolumeProfile;
        public string OnLoadCutscene;
    }
}