using Sheldier.Actors.Hand;
using Sheldier.Setup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorAttackModule : SerializedMonoBehaviour, IExtraActorModule
    {
        private ActorInputController _inputController;

        [SerializeField] private ActorsHand actorsHand;
        private ActorNotifyModule _notifier;
        public int Priority => 0;

        public void Initialize(ActorInternalData data)
        {
            _notifier = data.Notifier;
            _inputController = data.ActorInputController;
            _inputController.OnAttackButtonPressed += AttackPressed;
            _inputController.OnReloadButtonPressed += ReloadPressed;
        }

        private void ReloadPressed()
        {
            if (!actorsHand.IsEquipped)
                return;
            _notifier.NotifyReloading();
        }

        private void AttackPressed()
        {
            if (!actorsHand.IsEquipped)
                return;
            _notifier.NotifyAttack(_inputController.CursorScreenDirection.normalized);
        }

        public void Dispose()
        {
            _inputController.OnReloadButtonPressed -= ReloadPressed;
            _inputController.OnAttackButtonPressed -= AttackPressed;

        }
    }
}