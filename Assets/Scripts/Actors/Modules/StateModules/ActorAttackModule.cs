using Sheldier.Actors.Inventory;
using Sirenix.OdinInspector;

namespace Sheldier.Actors
{
    public class ActorAttackModule : SerializedMonoBehaviour, IExtraActorModule
    {
        private ActorInputController _inputController;

        private ActorNotifyModule _notifier;
        private ActorsInventoryModule _inventoryModule;
        public int Priority => 0;

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