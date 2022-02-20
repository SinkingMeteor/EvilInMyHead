using Sheldier.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    [CreateAssetMenu(menuName = "Sheldier/Items/Projectile", fileName = "ProjectileConfig")]
    public class ProjectileConfig : BaseScriptableObject
    {
        public Sprite Icon => _icon;
        public float Duration => _duration;
        public float Speed => _speed;
        
        [SerializeField][Range(0.1f, 10.0f)] private float _duration;
        [SerializeField][Range(0.1f, 10.0f)] private float _speed;
        [SerializeField][PreviewField]private Sprite _icon;
    }
}