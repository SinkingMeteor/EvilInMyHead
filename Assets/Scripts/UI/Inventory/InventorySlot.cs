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
        public ItemDynamicConfigData Item => _item;
        public Transform Transform => transform;

        public RectTransform RectTransform => rectTransform;
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform itemInfoTransform;
        [SerializeField] private TextMeshProUGUI itemInfoTMP;
        [SerializeField] private Image itemIcon;

        private ItemDynamicConfigData _item;
        private AssetProvider<Sprite> _spriteLoader;

        public void Initialize(IPool<InventorySlot> pool)
        {
            InitializeAnimations();
            itemInfoTransform.gameObject.SetActive(false);
        }

        public void SetDependencies(AssetProvider<Sprite> spriteLoader)
        {
            _spriteLoader = spriteLoader;
        }
        
        public void SetItem(ItemDynamicConfigData data)
        {
            _item = data;
            itemIcon.sprite = _spriteLoader.Get(_item.GameIcon);
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
            _item = null;
            itemIcon.sprite = null;
            itemInfoTransform.gameObject.SetActive(false);
        }
    }
}