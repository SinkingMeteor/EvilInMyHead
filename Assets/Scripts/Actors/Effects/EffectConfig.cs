using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Gameplay.Effects
{
    [CreateAssetMenu(fileName = "EffectConfig", menuName = "Sheldier/Effects/EffectConfig")]
    public class EffectConfig : BaseScriptableObject
    {
        public Sprite EffectIcon => _effectIcon;
        public string EffectName => _effectName;
        public string EffectDesription => _effectDescription;
        public ActorEffectType EffectType => _effectType;
        public float Duration => _duration;

        [SerializeField][PreviewField] private Sprite _effectIcon;
        [SerializeField] private string _effectName;
        [SerializeField][Multiline] private string _effectDescription;
        [Range(0.1f, 1000.0f)] private float _duration;
        [SerializeField] private ActorEffectType _effectType;
    }

    public enum ActorEffectType
    {
        Freeze = 0,
    }
}