using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


namespace Sheldier.Item
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Sheldier/Items/ItemConfig")]
    public class ItemConfig : IDConfig
    {
        public string Name => itemName;
        public string Description => itemDescription;
        public int Cost => _cost;
        public Sprite Icon => itemIcon;

        [OdinSerialize][PreviewField(100, ObjectFieldAlignment.Center)][HideLabel] private Sprite itemIcon;
        [SerializeField][Multiline] private string itemName;
        [SerializeField][Multiline] private string itemDescription;
        [SerializeField] private int _cost;
    }
}
