using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using Sirenix.Serialization;
using UnityEngine;

namespace Sheldier.Common.Animation
{
    [CreateAssetMenu(menuName = "Sheldier/Animation/AnimationCollection", fileName = "AnimationCollection")]
    public class ActorAnimationCollection : BaseScriptableObject
    {
        public IReadOnlyDictionary<AnimationType, AnimationData> AnimationCollection => animationCollection;
        [OdinSerialize] private Dictionary<AnimationType, AnimationData> animationCollection;
    }
}