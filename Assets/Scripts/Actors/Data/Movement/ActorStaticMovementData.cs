using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticMovementData : IDatabaseItem
    {
        public string ID => NameID;

        public string NameID;
        public string StepSound;
        public int Speed;
    }
}