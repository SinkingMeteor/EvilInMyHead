using Sirenix.OdinInspector;
using UnityEngine;


namespace Sheldier.Item
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Sheldier/Items/ItemConfig")]
    public abstract class ItemConfig : IDConfig
    {
        public string Name => itemName;
        public string Description => itemDescription;
        public int Cost => _cost;
        public Sprite Icon => itemIcon;
        public ItemType ItemType => itemType;
        public abstract ItemGroup ItemGroup { get; }

        [SerializeField] private ItemType itemType;
        [SerializeField][PreviewField(100, ObjectFieldAlignment.Center)] private Sprite itemIcon;
        [SerializeField][Multiline] private string itemName;
        [SerializeField][Multiline] private string itemDescription;
        [SerializeField] private int _cost;
        [SerializeField][EnumToggleButtons] private ItemGroup _itemGroup;
    }
}
