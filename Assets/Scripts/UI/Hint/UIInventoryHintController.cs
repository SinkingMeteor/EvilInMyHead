using System;
using System.Collections.Generic;
using Sheldier.Actors.Inventory;
using Sheldier.Common;
using Sheldier.Data;
using Sheldier.Item;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class UIInventoryHintController : UIHintController<ItemDynamicConfigData, InventoryHintPerformType>
    {
        protected override IUIItemSwitcher<ItemDynamicConfigData> ItemSwitcher => itemSwitcher;

        [OdinSerialize] private InventoryView itemSwitcher;
        private Inventory _inventory;
        private IInventoryInputProvider _inventoryInputProvider;
        private Database<ItemDynamicConfigData> _dynamicConfigDatabase;
        private Database<UIPerformStaticData> _uiPerformStaticDatabase;


        public override void Initialize()
        {
            base.Initialize();
            
            _conditionDictionary = new Dictionary<InventoryHintPerformType, Func<string, bool>>()
            {
                {InventoryHintPerformType.Use, id => _dynamicConfigDatabase.Get(id).IsUsable},
                {InventoryHintPerformType.Remove, id => !_dynamicConfigDatabase.Get(id).IsQuest},
                {InventoryHintPerformType.Equip, id => _dynamicConfigDatabase.Get(id).IsEquippable},
            };
            _performDictionary = new Dictionary<InventoryHintPerformType, Action>()
            {
                {InventoryHintPerformType.Use, () => {_inventory.UseItem(_currentItemId); itemSwitcher.Refresh();}},
                {InventoryHintPerformType.Remove, () => {_inventory.RemoveItem(_currentItemId); itemSwitcher.Refresh();}},
                {InventoryHintPerformType.Equip, () => {_inventory.EquipItem(_currentItemId); itemSwitcher.Refresh();}},
            };
            _hintsCollection = new Dictionary<InventoryHintPerformType, UIHint>();
        }
        [Inject]
        private void InjectDependencies(Inventory inventory, IInventoryInputProvider inventoryInputProvider, Database<ItemDynamicConfigData> dynamicConfigDatabase,
            Database<UIPerformStaticData> uiPerformStaticDatabase)
        {
            _uiPerformStaticDatabase = uiPerformStaticDatabase;
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _inventoryInputProvider = inventoryInputProvider;
            _inventory = inventory;
        }
        public override void OnActivated()
        {
            _inventoryInputProvider.UIUseItemButton.OnPressed += OnUseButtonPressed;
            _inventoryInputProvider.UIUseItemButton.OnReleased += OnUseButtonReleased;
            _inventoryInputProvider.UIRemoveItemButton.OnPressed += OnRemoveButtonPressed;
            _inventoryInputProvider.UIRemoveItemButton.OnReleased += OnRemoveButtonReleased;
            _inventoryInputProvider.UIEquipItemButton.OnPressed += OnEquipButtonPressed;
            _inventoryInputProvider.UIEquipItemButton.OnReleased += OnEquipButtonReleased;
            base.OnActivated();
        }

        public override void OnLanguageChanged()
        {
            foreach (var uiHint in _hintsCollection)
            {
                string text = _localizationProvider.LocalizedText[_uiPerformStaticDatabase.Get(uiHint.Key.ToString()).Localization];
                uiHint.Value.SetTitle(text);
            }
        }

        public override void OnDeviceChanged()
        {
            foreach (var uiHint in _hintsCollection)
            {
                Sprite icon = _bindIconProvider.GetActionInputSprite(GetActionType(uiHint.Key));
                uiHint.Value.SetIconImage(icon);
            }
        }

        public override void OnDeactivated()
        {
            _inventoryInputProvider.UIUseItemButton.OnPressed -= OnUseButtonPressed;
            _inventoryInputProvider.UIUseItemButton.OnReleased -= OnUseButtonReleased;
            _inventoryInputProvider.UIRemoveItemButton.OnPressed -= OnRemoveButtonPressed;
            _inventoryInputProvider.UIRemoveItemButton.OnReleased -= OnRemoveButtonReleased;
            _inventoryInputProvider.UIEquipItemButton.OnPressed -= OnEquipButtonPressed;
            _inventoryInputProvider.UIEquipItemButton.OnReleased -= OnEquipButtonReleased;
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

        private void OnEquipButtonPressed()
        {
            if (!_hintsCollection.ContainsKey(InventoryHintPerformType.Equip))
                return;
            _hintsCollection[InventoryHintPerformType.Equip].WaitAndPerform(_performDictionary[InventoryHintPerformType.Equip]);
        }
        private void OnEquipButtonReleased()
        {
            if (!_hintsCollection.ContainsKey(InventoryHintPerformType.Equip))
                return;
            _hintsCollection[InventoryHintPerformType.Equip].StopWaiting();
        }  
        
        protected override void CreateHint(InventoryHintPerformType conditionKey)
        {
            UIHint hint = _uiHintPool.GetFromPool();
            hint.Transform.SetParent(parentContainer);
            hint.Transform.localScale = Vector3.one;
            hint.SetIconImage(_bindIconProvider.GetActionInputSprite(GetActionType(conditionKey)));
            hint.SetTitle(_localizationProvider.LocalizedText[_uiPerformStaticDatabase.Get(conditionKey.ToString()).Localization]);
            
            _hintsCollection.Add(conditionKey, hint);
        }

        private InputActionType GetActionType(InventoryHintPerformType performType)
        {
            return performType switch
            {
                InventoryHintPerformType.Use => InputActionType.UseItem,
                InventoryHintPerformType.Remove => InputActionType.RemoveItem,
                InventoryHintPerformType.Equip => InputActionType.EquipItem,
                _ => throw new ArgumentOutOfRangeException(nameof(performType), performType, null)
            };
        }
    }
}