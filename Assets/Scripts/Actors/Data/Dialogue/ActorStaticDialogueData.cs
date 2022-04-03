using System;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public struct ActorStaticDialogueData : IDatabaseItem
    {
        public string ID => NameID;
        public string NameID;
        public float TypeSpeed;
        public float TextColorR;
        public float TextColorG;
        public float TextColorB;
    }
}