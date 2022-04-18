using System;
using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Data;
using Sheldier.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sheldier.UI
{
    public class InventoryView : MonoBehaviour, IUISimpleItemSwitcher, ILocalizationListener, IFontRequier, IUIActivatable, ITickListener
    {
        public FontType FontTypeRequirer => FontType.DefaultPixelFont7;
        public event Action<string> OnCurrentItemChanged;
        
        [SerializeField] private Image selectedItemImage;
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI descriptionTMP;
        [SerializeField] private UIRadialPointer radialPointer;
        [SerializeField] private RectTransform slotsParent;
        [SerializeField] private float slotsSpawnDistance = 4.0f;

        private Database<ItemStaticInventorySlotData> _staticInventorySlotDatabase;
        private Database<ItemDynamicConfigData> _dynamicConfigDatabase;
        private ILocalizationProvider _localizationProvider;
        private List<InventorySlot> _slotsCollection;
        private IPool<InventorySlot> _inventorySlotPool;
        private AssetProvider<Sprite> _spriteLoader;
        private IFontProvider _fontProvider;
        private InventorySlot _selectedSlot;
        private Inventory _inventory;

        private const string CANCEL_ID = "Cancel";
        
        [Inject]
        private void InjectDependencies(IPool<InventorySlot> inventorySlotPool, Inventory inventory, ILocalizationProvider localizationProvider, IFontProvider fontProvider,
            Database<ItemDynamicConfigData> dynamicConfigDatabase, Database<ItemStaticInventorySlotData> staticInventorySlotDatabase, AssetProvider<Sprite> spriteLoader)
        {
            _spriteLoader = spriteLoader;
            _staticInventorySlotDatabase = staticInventorySlotDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _fontProvider = fontProvider;
            _localizationProvider = localizationProvider;
            _inventory = inventory;
            _inventorySlotPool = inventorySlotPool;
        }

        public void Initialize()
        {
            var font = _fontProvider.GetActualFont(FontTypeRequirer);
            titleTMP.font = font;
            descriptionTMP.font = font;
            _fontProvider.AddListener(this);
        }
        public void OnActivated()
        {
            var inventoryItems = GetItemsList();
            _localizationProvider.AddListener(this);
            radialPointer.SetSegments(inventoryItems.Count);
            _slotsCollection = new List<InventorySlot>();
            int counter = 0;
            foreach(var item in inventoryItems)
            {
                InventorySlot slot = InstantiateSlot(item.Key);
                _slotsCollection.Add(slot);
                var offsetVector =  counter.UnitVectorFromSegment(inventoryItems.Count) * slotsSpawnDistance;
                slot.RectTransform.anchoredPosition = slotsParent.anchoredPosition + offsetVector;
                counter++;
            }
        }
        public void OnLanguageChanged()
        {
            if (_selectedSlot == null)
                CleanInfo();
            else
                SetNewInfo(_dynamicConfigDatabase.Get(_selectedSlot.ID).TypeName);                
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
            IReadOnlyDictionary<string, ItemDynamicConfigData> itemsList = GetItemsList();
            radialPointer.SetSegments(itemsList.Count);
            for (int i = 0; i < _slotsCollection.Count; i++)
            {
                if (!itemsList.ContainsKey(_slotsCollection[i].ID))
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
        public void OnDeactivated()
        {
            _localizationProvider.RemoveListener(this);
            for (int i = 0; i < _slotsCollection.Count; i++)
            {
                _inventorySlotPool.SetToPull(_slotsCollection[i]);
            }
        }
        public void UpdateFont(TMP_FontAsset textAsset)
        {
            titleTMP.font = textAsset;
            descriptionTMP.font = textAsset;
        }

        public void Dispose()
        {
            _fontProvider.RemoveListener(this);
        }
        private IReadOnlyDictionary<string, ItemDynamicConfigData> GetItemsList()
        {
            Dictionary<string, ItemDynamicConfigData> inventoryItems = new Dictionary<string, ItemDynamicConfigData>();
            foreach (var inventoryGroup in _inventory.ItemsCollection)
                foreach (var data in inventoryGroup.Value.Items)
                    inventoryItems.Add(data.Key, data.Value);
            return inventoryItems;
        }
        private InventorySlot InstantiateSlot(string id)
        {
            InventorySlot slot = _inventorySlotPool.GetFromPool();
            slot.transform.SetParent(slotsParent);
            slot.SetItem(id);
            slot.transform.localScale = Vector3.one;
            return slot;
        }


        private void SelectNewSlot(InventorySlot slot)
        {
            DeselectOldSlot();
            _selectedSlot = slot;
            _selectedSlot.Select();
            OnCurrentItemChanged?.Invoke(slot.ID);
            SetNewInfo(_dynamicConfigDatabase.Get(_selectedSlot.ID).TypeName);
        }

        private void CleanInfo()
        {
            DeselectOldSlot();
            SetNewInfo(CANCEL_ID);
        }

        private void DeselectOldSlot()
        {
            if (_selectedSlot != null)
            {
                _selectedSlot.Deselect();
                _selectedSlot = null;
            }

        }
        private void SetNewInfo(string typeID)
        {
            var inventorySlotData = _staticInventorySlotDatabase.Get(typeID);
            titleTMP.text = _localizationProvider.LocalizedText[inventorySlotData.ItemTitle];
            descriptionTMP.text = _localizationProvider.LocalizedText[inventorySlotData.ItemDescription];
            selectedItemImage.sprite = _spriteLoader.Get(inventorySlotData.Icon);
        }
        private void OnDrawGizmos()
        {
            Gizmos.matrix = slotsParent.localToWorldMatrix;
            Gizmos.DrawWireSphere(slotsParent.anchoredPosition, slotsSpawnDistance);
        }


 
    }
}