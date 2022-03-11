using System;
using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Item;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class UIInventoryHintController : UIHintController<SimpleItem, InventoryHintPerformType>
    {
        protected override IUIItemSwitcher<SimpleItem> ItemSwitcher => itemSwitcher;

        [OdinSerialize] private InventoryView itemSwitcher;
        [OdinSerialize] private ItemSlotMap itemSlotMap;
        private Inventory _inventory;


        public override void Initialize(IUIInputProvider inputProvider)
        {
            base.Initialize(inputProvider);
            _conditionDictionary = new Dictionary<InventoryHintPerformType, Func<SimpleItem, bool>>()
            {
                {InventoryHintPerformType.Use, item => !item.IsStackable},
                {InventoryHintPerformType.Remove, item => !item.IsQuest}
            };
            _performDictionary = new Dictionary<InventoryHintPerformType, Action>()
            {
                {InventoryHintPerformType.Use, () => {_inventory.UseItem(_currentItem); itemSwitcher.Refresh();}},
                {InventoryHintPerformType.Remove, () => {_inventory.RemoveItem(_currentItem); itemSwitcher.Refresh();}}
            };
            _hintsCollection = new Dictionary<InventoryHintPerformType, UIHint>();
        }
        [Inject]
        private void InjectDependencies(Inventory inventory)
        {
            _inventory = inventory;
        }
        public override void OnActivated()
        {
            _uiInputProvider.UIUseItemButton.OnPressed += OnUseButtonPressed;
            _uiInputProvider.UIUseItemButton.OnReleased += OnUseButtonReleased;
            _uiInputProvider.UIRemoveItemButton.OnPressed += OnRemoveButtonPressed;
            _uiInputProvider.UIRemoveItemButton.OnReleased += OnRemoveButtonReleased;
            base.OnActivated();
        }

        public override void OnLanguageChanged()
        {
            foreach (var uiHint in _hintsCollection)
            {
                string text = _localizationProvider.LocalizedText[itemSlotMap.HintTitleMap[uiHint.Key]];
                uiHint.Value.SetTitle(text);
            }
        }
        public override void OnDeactivated()
        {
            _uiInputProvider.UIUseItemButton.OnPressed -= OnUseButtonPressed;
            _uiInputProvider.UIUseItemButton.OnReleased -= OnUseButtonReleased;
            _uiInputProvider.UIRemoveItemButton.OnPressed -= OnRemoveButtonPressed;
            _uiInputProvider.UIRemoveItemButton.OnReleased -= OnRemoveButtonReleased;
            base.OnDeactivated();
        }

        private void OnRemoveButtonPressed()
        {
            if (!_hintsCollection.ContainsKey(InventoryHintPerformType.Remove))
                return;
            _hintsCollection[InventoryHintPerformType.Remove].WaitAndPerform(_performDictionary[InventoryHintPerformType.Remove]);
        }
        private void OnRemoveButtonReleased()
        {
            if (!_hintsCollection.ContainsKey(InventoryHintPerformType.Remove))
                return;
            _hintsCollection[InventoryHintPerformType.Remove].StopWaiting();
        }
        private void OnUseButtonPressed()
        {
            if (!_hintsCollection.ContainsKey(InventoryHintPerformType.Use))
                return;
            _hintsCollection[InventoryHintPerformType.Use].WaitAndPerform(_performDictionary[InventoryHintPerformType.Use]);
        }
        private void OnUseButtonReleased()
        {
            if (!_hintsCollection.ContainsKey(InventoryHintPerformType.Use))
                return;
            _hintsCollection[InventoryHintPerformType.Use].StopWaiting();
        }

        protected override void CreateHint(InventoryHintPerformType conditionKey)
        {
            UIHint hint = _uiHintPool.GetFromPool();
            hint.Transform.SetParent(parentContainer);
            hint.Transform.localScale = Vector3.one;
            hint.SetIconImage(_bindIconProvider.GetActionInputSprite(GetActionType(conditionKey)));
            hint.SetTitle(_localizationProvider.LocalizedText[itemSlotMap.HintTitleMap[conditionKey]]);
            
            _hintsCollection.Add(conditionKey, hint);
        }

        private InputActionType GetActionType(InventoryHintPerformType performType)
        {
            return performType switch
            {
                InventoryHintPerformType.Use => InputActionType.UseItem,
                InventoryHintPerformType.Remove => InputActionType.RemoveItem
            };
        }
    }
}