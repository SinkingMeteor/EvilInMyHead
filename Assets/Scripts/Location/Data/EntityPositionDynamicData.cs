using System;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.GameLocation
{
    [Serializable]
    public class EntityPositionDynamicData : IDatabaseItem
    {
        public string ID => Guid;
        
        public string Guid;
        public Vector2 Position;

        public EntityPositionDynamicData(string guid)
        {
            Guid = guid;
        }
    }
}