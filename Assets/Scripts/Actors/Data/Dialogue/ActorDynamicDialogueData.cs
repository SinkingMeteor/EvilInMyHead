using System;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicDialogueData : IDatabaseItem
    {
        public string ID => Guid;
        public string Guid;
        public string NameID;
        public float TypeSpeed;
        public ActorDynamicDialogueData(string guid, ActorStaticDialogueData staticDialogueData)
        {
            Guid = guid;
            NameID = staticDialogueData.NameID;
            TypeSpeed = staticDialogueData.TypeSpeed;
        }
    }
}