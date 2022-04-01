using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticConfigData : IDatabaseItem
    {
        public string ID => NameID;

        public string NameID;
        public string ActorAppearance;
        public string ActorAvatar;
    }
}