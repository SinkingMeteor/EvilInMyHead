using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    [CreateAssetMenu(menuName = "Sheldier/Actors/ActorsMap", fileName = "ActorsMap")]
    public class ActorsMap : BaseScriptableObject
    {
        public Dictionary<ActorType, Actor> Actors => _actorsCollection;
        
        [OdinSerialize] private Dictionary<ActorType, Actor> _actorsCollection;
    }
}