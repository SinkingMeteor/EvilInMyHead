using System;
using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Actors.Inventory;
using Sheldier.Factories;
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

        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemConfig itemReference;
        [SerializeField] private Material onInteractMaterial;
        [SerializeField] private int amount = 1;

        private Material _defaulMaterial;
        private ItemFactory _itemFactory;

        public void Initialize(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            spriteRenderer.sprite = itemReference.Icon;
            _defaulMaterial = spriteRenderer.sharedMaterial;
        }


        public void OnEntered()
        {
            spriteRenderer.sharedMaterial = onInteractMaterial;
        }

        public bool OnInteracted(Actor actor)
        {
            SimpleItem item = _itemFactory.GetItem(itemReference);
            item.ItemAmount.Set(amount);
            InventoryOperationReport report = actor.InventoryModule.AddItem(item);
            if (!report.IsCompleted)
                amount -= report.Amount;
            else
            {
                Deactivate();
                return true;
            }

            return false;
        }

        public void OnExit()
        {
            spriteRenderer.sharedMaterial = _defaulMaterial;
        }
        private void Deactivate() => gameObject.SetActive(false);

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }

        private void OnValidate()
        {
            if (itemReference == null) return;
            amount = Mathf.Clamp(amount, 1, itemReference.MaxStack);
        }

        [Button("Setup")]
        private void SetupPlaceholder()
        {
            uniqueID = GetComponent<UniqueID>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        #endif
    }
    
    
}