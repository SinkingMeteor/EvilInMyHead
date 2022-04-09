using System;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicConfigData : IDatabaseItem
    {
        public string ID => Guid;

        public string Guid;
        public string TypeName;
        public Vector2 Position;

        public ActorDynamicConfigData(string guid, ActorStaticConfigData staticConfigData)
        {
            Guid = guid;
            TypeName = staticConfigData.TypeName;
        }
    }
}