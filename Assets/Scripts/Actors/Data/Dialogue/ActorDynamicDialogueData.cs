using System;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [Serializable]
    public class ActorDynamicDialogueData : IDatabaseItem
    {
        public string ID => Guid;
        public Color TextColor => new Color(ColorData[0], ColorData[1], ColorData[2]);
        
        public string Guid;
        public string NameID;
        public float TypeSpeed;
        public float[] ColorData; 
        
        public ActorDynamicDialogueData(string guid, ActorStaticDialogueData staticDialogueData)
        {
            Guid = guid;
            NameID = staticDialogueData.NameID;
            TypeSpeed = staticDialogueData.TypeSpeed;
            ColorData = new[]
                {staticDialogueData.TextColorR, staticDialogueData.TextColorG, staticDialogueData.TextColorB};
        }
    }
}