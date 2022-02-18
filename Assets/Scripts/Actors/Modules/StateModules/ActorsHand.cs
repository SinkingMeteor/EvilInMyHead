using System;
using Sheldier.Item;
using Sheldier.Setup;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorsHand : MonoBehaviour, IExtraActorModule, IActorsHand
    {
        public bool IsEquipped => _currentEquippable != null;
        public int Priority => 0;
        public event Action<Vector2> OnHandAttack;
        public event Action OnHandReload;
        public event Action OnHandUse;

        [SerializeField] private Transform _actorHandObject;
        private ActorTransformHandler _transformHandler;
        private IGrabNotifier _grabNotifier;
        private IEquippable _currentEquippable;

        public void Initialize(IActorModuleCenter moduleCenter)
        {
            _transformHandler = moduleCenter.ActorTransformHandler;
            _grabNotifier = moduleCenter.GrabNotifier;
            _grabNotifier.OnItemPickedUp += OnItemPickedUp;
        }
        public void AttackByEquip(Vector2 direction) => OnHandAttack?.Invoke(direction);
        public void ReloadEquip() => OnHandReload?.Invoke();
        public void UseEquip() => OnHandUse?.Invoke();
        public void Tick()
        {
        }

        public void RotateHand(Vector2 dir)
        {
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            if (!_transformHandler.LooksToRight)
                angle -= 180;
            _actorHandObject.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        public void Equip(IEquippable equippable)
        {

            _currentEquippable = equippable;
            equippable.OnEquip(this);
            equippable.Transform.SetParent(_actorHandObject.transform);
            equippable.Transform.localScale = new Vector3(1, 1, 1);
            equippable.Transform.localPosition = Vector3.zero;
        }
        public void UnEquip()
        {
            if (_currentEquippable == null)
                return;
            
            _currentEquippable.OnUnEquip();
            _currentEquippable = null;
        }
        private void OnItemPickedUp(PickupObject item)
        {
            if (IsEquipped)
                return;
            
            if (item is not IEquippable equipable)
                return;
            
            Equip(equipable);
        }
        private void OnDestroy()
        {
            if (!GameGlobalSettings.IsStarted) return;
            _grabNotifier.OnItemPickedUp -= OnItemPickedUp;
        }

 
    }
}