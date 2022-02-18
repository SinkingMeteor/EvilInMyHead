using Sheldier.Setup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorAttackModule : SerializedMonoBehaviour, IExtraActorModule
    {
        private bool _isAttacks;
        
        private ActorInputController _inputController;

        [SerializeField] private ActorsHand actorsHand;
        public int Priority => 0;

        public void Initialize(IActorModuleCenter moduleCenter)
        {
            _inputController = moduleCenter.ActorInputController;
            _inputController.OnAttackButtonPressed += AttackPressed;
            _inputController.OnAttackButtonReleased += AttackReleased;
        }
        private void AttackReleased() => _isAttacks = false;

        private void AttackPressed()
        {
            if (!actorsHand.IsEquipped)
                return;
            actorsHand.AttackByEquip(_inputController.CursorScreenDirection.normalized);
        }

        public void Tick()
        {
        }

        private void OnDestroy()
        {
            if (!GameGlobalSettings.IsStarted) return;
            _inputController.OnAttackButtonPressed -= AttackPressed;
            _inputController.OnAttackButtonReleased -= AttackReleased;
        }
    }
}