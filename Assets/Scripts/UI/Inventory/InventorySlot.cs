using System;
using Sheldier.Common.Pool;
using Sheldier.Constants;
using Sheldier.Data;
using Sheldier.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class InventorySlot : SelectableSlot, IPoolObject<InventorySlot>
    {
        public string ID => _guid;
        public Transform Transform => transform;

        public RectTransform RectTransform => rectTransform;
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform itemInfoTransform;
        [SerializeField] private TextMeshProUGUI itemInfoTMP;
        [SerializeField] private Image itemIcon;

        private Database<ItemDynamicConfigData> _itemDynamicConfigDatabase;
        private AssetProvider<Sprite> _spriteLoader;
        private string _guid;

        public void Initialize(IPool<InventorySlot> pool)
        {
            InitializeAnimations();
            itemInfoTransform.gameObject.SetActive(false);
        }

        public void SetDependencies(AssetProvider<Sprite> spriteLoader, Database<ItemDynamicConfigData> itemDynamicConfigDatabase)
        {
            _itemDynamicConfigDatabase = itemDynamicConfigDatabase;
            _spriteLoader = spriteLoader;
        }
        
        public void SetItem(string guid)
        {
            _guid = guid;
            var dynamicData = _itemDynamicConfigDatabase.Get(guid);
            itemIcon.sprite = _spriteLoader.Get(dynamicData.GameIcon);
            UpdateInfo();
            itemInfoTransform.gameObject.SetActive(true);
        }

        public void UpdateInfo()
        {

        }
        public void OnInstantiated()
        {
        }

        public void Dispose()
        {
            
        }

        public void Reset()
        {
            _guid = null;
            itemIcon.sprite = null;
            itemInfoTransform.gameObject.SetActive(false);
        }
    }
}