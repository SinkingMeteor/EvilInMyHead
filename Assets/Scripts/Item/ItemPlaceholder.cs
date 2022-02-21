using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Item
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemPlaceholder : MonoBehaviour, IUniqueID, IInteractReceiver
    {
        public Transform Transform => transform;
        public string ID => uniqueID.ID;
        public ItemType Reference => itemReference;

        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemType itemReference;
        [SerializeField] private Material OnInteractMaterial;

        private ItemConfig _item;
        private Material _defaulMaterial;
        
        public void Initialize(ItemConfig itemConfig)
        {
            _item = itemConfig;
            spriteRenderer.sprite = itemConfig.Icon;
            _defaulMaterial = spriteRenderer.sharedMaterial;
        }

        public void Deactivate() => gameObject.SetActive(false);

        public void OnEntered()
        {
            spriteRenderer.sharedMaterial = OnInteractMaterial;
        }

        public void OnInteracted(ActorNotifyModule notifier)
        {
            notifier.NotifyPickUpItem(_item);
            Deactivate();
        }

        public void OnExit()
        {
            spriteRenderer.sharedMaterial = _defaulMaterial;
        }
        
        [Button("Setup")]
        private void SetupPlaceholder()
        {
            uniqueID = GetComponent<UniqueID>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}