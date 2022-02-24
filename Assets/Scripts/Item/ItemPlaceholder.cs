using System;
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
        public ItemConfig Reference => itemReference;

        [SerializeField] private UniqueID uniqueID;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemConfig itemReference;
        [SerializeField] private Material OnInteractMaterial;

        private SimpleItem _simpleItem;
        private Material _defaulMaterial;
        
        public void Initialize(SimpleItem simpleItem)
        {
            _simpleItem = simpleItem;
            spriteRenderer.sprite = simpleItem.ItemConfig.Icon;
            _defaulMaterial = spriteRenderer.sharedMaterial;
        }

        public void Deactivate() => gameObject.SetActive(false);

        public void OnEntered()
        {
            spriteRenderer.sharedMaterial = OnInteractMaterial;
        }

        public void OnInteracted(Actor actor)
        {
            if (!actor.InventoryModule.AddItem(_simpleItem)) return;
            actor.Notifier.NotifyAddedItemToInventory(_simpleItem);
            Deactivate();
        }

        public void OnExit()
        {
            spriteRenderer.sharedMaterial = _defaulMaterial;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }

        [Button("Setup")]
        private void SetupPlaceholder()
        {
            uniqueID = GetComponent<UniqueID>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
    
    
}