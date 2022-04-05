using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticMovementData : IDatabaseItem
    {
        public string ID => TypeName;

        public string TypeName;
        public string StepSound;
        public float Speed;
    }
}