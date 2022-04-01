using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicMovementData : IDatabaseItem
    {
        public string ID => Guid;
        public string Guid;
        public string NameID;
        public string StepSound;
        public int Speed;

        public ActorDynamicMovementData(string guid, ActorStaticMovementData staticMovementData)
        {
            Guid = guid;
            NameID = staticMovementData.NameID;
            StepSound = staticMovementData.StepSound;
            Speed = staticMovementData.Speed;
        }
    }
}