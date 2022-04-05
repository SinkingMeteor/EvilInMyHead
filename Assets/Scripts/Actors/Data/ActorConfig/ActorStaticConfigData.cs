using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticConfigData : IDatabaseItem
    {
        public string ID => TypeName;

        public string TypeName;
        public string ActorAppearance;
        public string ActorAvatar;
    }
}