using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/ActorsMap", fileName = "ActorsMap")]
    public class ActorsMap : BaseScriptableObject
    {
        public IReadOnlyDictionary<ActorConfig, ActorData> Actors => _actorsCollection;
        
        [OdinSerialize] private Dictionary<ActorConfig, ActorData> _actorsCollection;
    }
}