using Sheldier.Actors;
using Sheldier.Actors.Data;
using UnityEngine;

namespace Sheldier.Gameplay.Effects
{
    public class FreezeMovementEffect : IEffect
    {
        public ActorEffectType EffectType => ActorEffectType.Freeze;
        public bool IsExpired => _timeLeft <= 0;

        private float _timeLeft;
        private Actor _owner;

        public void Setup(Actor owner, float duration)
        {
            _owner = owner;
            _timeLeft = duration;
        }

        public void Tick()
        {
            var movementDataModule = _owner.DataModule.MovementDataModule;
            movementDataModule.SetSpeed(movementDataModule.CurrentSpeed / 2);
            _timeLeft -= Time.deltaTime;
        }
        
        public IEffect Clone() => new FreezeMovementEffect();
        
    }
}