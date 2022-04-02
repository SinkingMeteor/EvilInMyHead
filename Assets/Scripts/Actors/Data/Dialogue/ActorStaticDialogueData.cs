using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticDialogueData : IDatabaseItem
    {
        public string ID => NameID;
        public string NameID;
        public float TypeSpeed;
    }
}