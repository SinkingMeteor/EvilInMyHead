using System;
using Sheldier.Common.Pool;
using Sheldier.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class InventorySlot : SelectableSlot, IPoolObject<InventorySlot>
    {
        public SimpleItem Item => _item;
        public Transform Transform => transform;

        public RectTransform RectTransform => rectTransform;
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform itemInfoTransform;
        [SerializeField] private TextMeshProUGUI itemInfoTMP;
        [SerializeField] private Image itemIcon;

        private SimpleItem _item;

        public void Initialize(IPoolSetter<InventorySlot> poolSetter)
        {
            InitializeAnimations();
            itemInfoTransform.gameObject.SetActive(false);
        }
        public void SetItem(SimpleItem item)
        {
            itemIcon.sprite = item.ItemConfig.Icon;
            _item = item;
            UpdateInfo();
            itemInfoTransform.gameObject.SetActive(true);
        }

        public void UpdateInfo()
        {
            string extraInfo = _item.GetExtraInfo();
            if (extraInfo == String.Empty) return;
            itemInfoTMP.text = extraInfo;

        }
        public void OnInstantiated()
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