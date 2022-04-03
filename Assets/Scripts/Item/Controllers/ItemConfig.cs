using Sirenix.OdinInspector;
using UnityEngine;


namespace Sheldier.Item
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Sheldier/Items/ItemConfig")]
    public abstract class ItemConfig : IDConfig
    {
        public int Cost => cost;
        public int MaxStack => maxStack;
        public Sprite Icon => itemIcon;
        public ItemType ItemType => itemType;
        public abstract ItemGroup ItemGroup { get; }

        [SerializeField] private ItemType itemType;
        [SerializeField][PreviewField(100, ObjectFieldAlignment.Center)] private Sprite itemIcon;
        [SerializeField] private int cost;
        [SerializeField] private int maxStack = 1;
    }
}
