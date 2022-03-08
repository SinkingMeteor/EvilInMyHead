using System;
using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Common.Pool;
using Sheldier.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryView : MonoBehaviour
    {
        
        [SerializeField] private Image selectedItemImage;
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI descriptionTMP;
        [SerializeField] private UIRadialPointer radialPointer;
        [SerializeField] private RectTransform slotsParent;
        [SerializeField] private ItemSlotMap itemSlotMap;
        [SerializeField] private float slotsSpawnDistance = 4.0f;

        private InventorySlot[] _slotsCollection;
        private InventorySlotPool _inventorySlotPool;
        private InventorySlot _selectedSlot;

        [Inject]
        private void InjectDependencies(InventorySlotPool inventorySlotPool)
        {
            _inventorySlotPool = inventorySlotPool;
        }
        public void Activate(List<SimpleItem> inventoryItems)
        {
            radialPointer.SetSegments(inventoryItems.Count);
            _slotsCollection = new InventorySlot[inventoryItems.Count];
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                InventorySlot slot = InstantiateSlot(inventoryItems[i]);
                _slotsCollection[i] = slot;
                var offsetVector =  i.UnitVectorFromSegment(inventoryItems.Count) * slotsSpawnDistance;
                slot.RectTransform.anchoredPosition = slotsParent.anchoredPosition + offsetVector;
            }
        }

        public SimpleItem GetCurrentSelectedItem()
        {
            if (_selectedSlot == null)
                return null;
            return _selectedSlot.Item;
        }
        public void Tick()
        {
            int slotIndex = radialPointer.CurrentSegment;

            if (slotIndex == -1)
            {
                CleanInfo();
                return;
            }

            if (slotIndex >= _slotsCollection.Length) 
                return;

            if (_slotsCollection[slotIndex] == _selectedSlot)
                return;

            SelectNewSlot(_slotsCollection[slotIndex]);
        }

        public void Deactivate()
        {
            for (int i = 0; i < _slotsCollection.Length; i++)
            {
                _inventorySlotPool.SetToPull(_slotsCollection[i]);
            }
        }
        private InventorySlot InstantiateSlot(SimpleItem inventoryItem)
        {
            InventorySlot slot = _inventorySlotPool.GetFromPool();
            slot.transform.SetParent(slotsParent);
            slot.SetItem(inventoryItem);
            slot.transform.localScale = Vector3.one;
            return slot;
        }
        private void SelectNewSlot(InventorySlot slot)
        {
            DeselectOldSlot();
            _selectedSlot = slot;
            _selectedSlot.Select();
            SetNewInfo();
        }

        private void CleanInfo()
        {
            DeselectOldSlot();
            titleTMP.text = itemSlotMap.CancelSlot.Title;
            descriptionTMP.text = itemSlotMap.CancelSlot.Description;
            selectedItemImage.sprite = itemSlotMap.CancelSlot.PreviewSprite;
        }

        private void DeselectOldSlot()
        {
            if (_selectedSlot != null)
            {
                _selectedSlot.Deselect();
                _selectedSlot = null;
            }

        }
        private void SetNewInfo()
        {
            ItemSlotData data = itemSlotMap.SlotMap[_selectedSlot.Item.ItemConfig];
            titleTMP.text = data.Title;
            descriptionTMP.text = data.Description;
            selectedItemImage.sprite = data.PreviewSprite;
        }
        private void OnDrawGizmos()
        {
            Gizmos.matrix = slotsParent.localToWorldMatrix;
            Gizmos.DrawWireSphere(slotsParent.anchoredPosition, slotsSpawnDistance);
        }
    }
}