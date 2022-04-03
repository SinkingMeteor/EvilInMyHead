using System;
using Sheldier.Actors;
using Sheldier.Actors.Interact;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
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
        public DataReference Reference => itemReference;

        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private DataReference itemReference;
        [SerializeField] private Material onInteractMaterial;
        [SerializeField] private int amount = 1;

        private Material _defaultMaterial;
        private ItemFactory _itemFactory;
        private ItemDynamicConfigData _dynamicConfigData;

        public void Initialize(ItemDynamicConfigData dynamicConfigData)
        {
            _dynamicConfigData = dynamicConfigData;
            _defaultMaterial = spriteRenderer.sharedMaterial;
        }

        public void SetItemIcon(Sprite icon)
        {
            spriteRenderer.sprite = icon;
        }
        public void OnEntered()
        {
            spriteRenderer.sharedMaterial = onInteractMaterial;
        }

        public bool OnInteracted(Actor actor)
        {
            _dynamicConfigData.Amount = amount;
            InventoryOperationReport report = actor.InventoryModule.AddItem(_dynamicConfigData);
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
            spriteRenderer.sharedMaterial = _defaultMaterial;
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
            amount = Mathf.Clamp(amount, 1, _dynamicConfigData.MaxStack);
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