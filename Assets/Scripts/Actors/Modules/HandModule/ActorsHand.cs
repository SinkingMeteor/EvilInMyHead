﻿using Sheldier.Common;
using Sheldier.Item;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule, ITickListener
    {
        
        [SerializeField] private HandView actorHandObject;
        
        private ActorTransformHandler _transformHandler;
        private TickHandler _tickHandler;
        private SimpleItem _currentItem;
        private NullItem _nullItem;
        private Actor _actor;

        public void Initialize(ActorInternalData data)
        {
            _actor = data.Actor;
            _transformHandler = data.ActorTransformHandler;
            _tickHandler = data.TickHandler;
            _nullItem = new NullItem();
            _currentItem = _nullItem;
            actorHandObject.SetDependencies(_tickHandler, _actor.DataModule.StateDataModule);
            actorHandObject.Initialize();
            _tickHandler.AddListener(this);
            _actor.InventoryModule.OnUseItem += Equip;
            _actor.OnWillRemoveControl += UnEquip;
        }
        public void Tick()
        {
            RotateHand();
            actorHandObject.Tick();
        }
        private void Equip(SimpleItem item)
        {
            if (!item.IsEquippable)
                return;
            if (_currentItem == item)
            {
                UnEquip();
                return;
            }
            if (_currentItem != _nullItem)
                _currentItem.Unequip();
            _currentItem = item;
            _currentItem.Equip(actorHandObject, _actor);
            _actor.InventoryModule.SetEquipped(true);
        }

        private void UnEquip()
        {
            if (_currentItem == _nullItem)
                return;
            _currentItem.Unequip();
            _actor.InventoryModule.SetEquipped(false);
            _currentItem = _nullItem;
        }

        private void RotateHand()
        {
            var dir = _currentItem.GetRotateDirection();
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f, 0f, angle), Time.deltaTime*7.0f); 
        }
        public void Dispose()
        {
            _actor.OnWillRemoveControl -= UnEquip;
            _actor.InventoryModule.OnUseItem -= Equip;
            _tickHandler.RemoveListener(this);
            actorHandObject.Dispose();
        }

      
    }
}