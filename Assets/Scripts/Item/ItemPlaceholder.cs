using Sheldier.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemPlaceholder : MonoBehaviour, IUniqueID
    {
        public string ID => uniqueID.ID;
        public ItemType Reference => itemReference;

        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemType itemReference;

        private ItemConfig _item;
        
        public void Initialize(ItemConfig itemConfig)
        {
            _item = itemConfig;
            spriteRenderer.sprite = itemConfig.Icon;
        }

        public void Deactivate() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out Actor actor))
            {
                actor.Notifier.NotifyPickUpItem(_item);
                Deactivate();
            }
        }

        [Button("Setup")]
        private void SetupPlaceholder()
        {
            uniqueID = GetComponent<UniqueID>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}