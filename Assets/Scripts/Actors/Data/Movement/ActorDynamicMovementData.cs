using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicMovementData : IDatabaseItem
    {
        public string ID => Guid;
        public string Guid;
        public string TypeName;
        public float Speed;

        public ActorDynamicMovementData(string guid, ActorStaticMovementData staticMovementData)
        {
            Guid = guid;
            TypeName = staticMovementData.TypeName;
            Speed = staticMovementData.Speed;
        }
    }
}