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

        public void Initialize(IActorModuleCenter moduleCenter)
        {
            _notifier = moduleCenter.Notifier;
            _inputController = moduleCenter.ActorInputController;
            _inputController.OnAttackButtonPressed += AttackPressed;
        }
        private void AttackPressed()
        {
            if (!actorsHand.IsEquipped)
                return;
            _notifier.NotifyAttack(_inputController.CursorScreenDirection.normalized);
        }

        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (!GameGlobalSettings.IsStarted) return;
            #endif
            _inputController.OnAttackButtonPressed -= AttackPressed;
        }
    }
}