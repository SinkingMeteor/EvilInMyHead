using System.Collections.Generic;
using Sheldier.Actors.Data;
using Sheldier.Common.Animation;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Actors
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/ActorsMap", fileName = "ActorsMap")]
    public class ActorsMap : BaseScriptableObject
    {
        public Dictionary<ActorAnimationCollection, ActorBuildData> Actors => _actorsCollection;
        
        [OdinSerialize] private Dictionary<ActorAnimationCollection, ActorBuildData> _actorsCollection;
    }
}