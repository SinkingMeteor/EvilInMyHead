using Sheldier.Common.Animation;
using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors.Data
{
    [CreateAssetMenu(menuName = "Sheldier/Actor/Config", fileName = "ActorConfig")]
    public class ActorConfig : BaseScriptableObject
    {
        public ActorType ActorType => actorType;
        public Sprite Avatar => actorAvatar;
        public string ActorName => actorName;
        public string ActorDescription => actorDescription;
        public ActorAnimationCollection DefaultAppearance => actorAppearance;
        
        [SerializeField] private ActorType actorType;
        [SerializeField][PreviewField(100, ObjectFieldAlignment.Center)] private Sprite actorAvatar;
        [SerializeField] [Multiline] private string actorName;
        [SerializeField] [Multiline] private string actorDescription;
        [SerializeField] private ActorAnimationCollection actorAppearance;
    }
}