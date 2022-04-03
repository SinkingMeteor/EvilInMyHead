using System;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticBuildData : IDatabaseItem
    {
        public string ID => NameID;

        public string NameID;
        public bool CanMove;
        public bool CanEquip;
        public bool IsEffectPerceptive;
        public bool CanInteract;
        public bool CanAttack;
        public bool CanJump;
        public string InteractType;
        public int InteractID;
    }
}