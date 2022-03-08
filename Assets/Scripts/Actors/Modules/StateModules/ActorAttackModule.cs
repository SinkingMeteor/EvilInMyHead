using Sheldier.Actors.Inventory;

namespace Sheldier.Actors
{
    public class ActorAttackModule : IExtraActorModule
    {
        private ActorInputController _inputController;

        private ActorNotifyModule _notifier;
        private ActorsInventoryModule _inventoryModule;

        public void Initialize(ActorInternalData data)
        {
            _notifier = data.Actor.Notifier;
            _inventoryModule = data.Actor.InventoryModule;
            _inputController = data.Actor.InputController;
            _inputController.OnAttackButtonPressed += AttackPressed;
            _inputController.OnReloadButtonPressed += ReloadPressed;
        }

        private void ReloadPressed()
        {
            if (!_inventoryModule.IsEquipped)
                return;
            _notifier.NotifyReloading();
        }

        private void AttackPressed()
        {
            
            if (!_inventoryModule.IsEquipped)
                return;
            _notifier.NotifyAttack();
        }

        public void Dispose()
        {
            _inputController.OnReloadButtonPressed -= ReloadPressed;
            _inputController.OnAttackButtonPressed -= AttackPressed;

        }
    }
}