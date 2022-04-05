using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicConfigData : IDatabaseItem
    {
        public string ID => Guid;

        public string Guid;
        public string TypeName;
        public string ActorAppearance;
        public string ActorAvatar;

        public ActorDynamicConfigData(string guid, ActorStaticConfigData staticConfigData)
        {
            Guid = guid;
            TypeName = staticConfigData.TypeName;
            ActorAppearance = staticConfigData.ActorAppearance;
            ActorAvatar = staticConfigData.ActorAvatar;
        }
    }
}