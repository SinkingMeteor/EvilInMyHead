using System;
using Sheldier.Factories;
using Sheldier.Item;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Actors.Inventory
{
    public class ActorsInventoryModule : MonoBehaviour, IExtraActorModule
    {
        public WeaponInventorySubModule WeaponInventorySubModule => _weaponInventorySubModule;
        public int Priority => 0;

        private WeaponInventorySubModule _weaponInventorySubModule;
        private ActorNotifyModule _notifier;


        public void Initialize(ActorInternalData data)
        {
            _notifier = data.Notifier;
            _notifier.OnItemPickedUp += AddItem;
            _weaponInventorySubModule = new WeaponInventorySubModule(data.ItemFactory, _notifier);
        }

        public void AddItem(ItemConfig item)
        {
            GetSubModule(item)();
        }

        private Action GetSubModule(ItemConfig item)
        {
            var group = item.ItemGroup;
            return group switch
            {
                ItemGroup.Weapon => () => _weaponInventorySubModule.AddItem(item)
            };
        }


        public void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _notifier.OnItemPickedUp -= AddItem;
        }


    }
}