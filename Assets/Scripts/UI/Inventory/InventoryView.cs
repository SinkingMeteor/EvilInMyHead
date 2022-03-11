using System;
using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryView : MonoBehaviour, IUISimpleItemSwitcher, ILocalizationListener
    {
        public event Action<SimpleItem> OnCurrentItemChanged;
        
        [SerializeField] private Image selectedItemImage;
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI descriptionTMP;
        [SerializeField] private UIRadialPointer radialPointer;
        [SerializeField] private RectTransform slotsParent;
        [SerializeField] private ItemSlotMap itemSlotMap;
        [SerializeField] private float slotsSpawnDistance = 4.0f;

        private ILocalizationProvider _localizationProvider;
        private List<InventorySlot> _slotsCollection;
        private InventorySlotPool _inventorySlotPool;
        private InventorySlot _selectedSlot;
        private Inventory _inventory;

        [Inject]
        private void InjectDependencies(InventorySlotPool inventorySlotPool, Inventory inventory, ILocalizationProvider localizationProvider)
        {
            _localizationProvider = localizationProvider;
            _inventory = inventory;
            _inventorySlotPool = inventorySlotPool;
        }
        public void Activate()
        {
            var inventoryItems = GetItemsList();
            _localizationProvider.AddListener(this);
            radialPointer.SetSegments(inventoryItems.Count);
            _slotsCollection = new List<InventorySlot>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                InventorySlot slot = InstantiateSlot(inventoryItems[i]);
                _slotsCollection.Add(slot);
                var offsetVector =  i.UnitVectorFromSegment(inventoryItems.Count) * slotsSpawnDistance;
                slot.RectTransform.anchoredPosition = slotsParent.anchoredPosition + offsetVector;
            }
        }
        public void OnLanguageChanged()
        {
            if (_selectedSlot == null)
                CleanInfo();
            else
                SetNewInfo();                
        }
        public void Tick()
        {
            int slotIndex = radialPointer.CurrentSegment;

            if (slotIndex == -1)
            {
                CleanInfo();
                OnCurrentItemChanged?.Invoke(null);
                return;
            }

            if (slotIndex >= _slotsCollection.Count) 
                return;

            if (_slotsCollection[slotIndex] == _selectedSlot)
                return;

            SelectNewSlot(_slotsCollection[slotIndex]);
        }
        public void Refresh()
        {
            List<SimpleItem> itemsList = GetItemsList();
            radialPointer.SetSegments(itemsList.Count);
            for (int i = 0; i < _slotsCollection.Count; i++)
            {
                if (!itemsList.Contains(_slotsCollection[i].Item))
                {
                    (_slotsCollection[i], _slotsCollection[^1]) = (_slotsCollection[^1], _slotsCollection[i]);
                    _inventorySlotPool.SetToPull(_slotsCollection[^1]);
                    _slotsCollection.RemoveAt(_slotsCollection.Count-1);
                    i -= 1;
                    continue;
                }
                _slotsCollection[i].UpdateInfo();
                var offsetVector =  i.UnitVectorFromSegment(itemsList.Count) * slotsSpawnDistance;
                _slotsCollection[i].RectTransform.anchoredPosition = slotsParent.anchoredPosition + offsetVector;
            }

            Tick();
        }
        public void Deactivate()
        {
            _localizationProvider.RemoveListener(this);
            for (int i = 0; i < _slotsCollection.Count; i++)
            {
                _inventorySlotPool.SetToPull(_slotsCollection[i]);
            }
        }
        private List<SimpleItem> GetItemsList()
        {
            List<SimpleItem> inventoryItems = new List<SimpleItem>();
            foreach (var inventoryGroup in _inventory.ItemsCollection)
                inventoryItems.AddRange(inventoryGroup.Value.Items);
            return inventoryItems;
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
            OnCurrentItemChanged?.Invoke(slot.Item);
            SetNewInfo();
        }

        private void CleanInfo()
        {
            DeselectOldSlot();
            titleTMP.text = _localizationProvider.LocalizedText[itemSlotMap.CancelSlot.Title];
            descriptionTMP.text = _localizationProvider.LocalizedText[itemSlotMap.CancelSlot.Description];
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
            titleTMP.text = _localizationProvider.LocalizedText[data.Title];
            descriptionTMP.text = _localizationProvider.LocalizedText[data.Description];
            selectedItemImage.sprite = data.PreviewSprite;
        }
        private void OnDrawGizmos()
        {
            Gizmos.matrix = slotsParent.localToWorldMatrix;
            Gizmos.DrawWireSphere(slotsParent.anchoredPosition, slotsSpawnDistance);
        }


    }
}