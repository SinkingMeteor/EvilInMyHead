using Sheldier.Actors.Data;
using UnityEngine;

namespace Sheldier.Gameplay.Effects
{
    public class FreezeMovementEffect : BaseEffect
    {
        public FreezeMovementEffect(int ID) : base(ID)
        { }

        public override void Tick()
        {
           // var movementDataModule = _owner.DataModule.MovementDataModule;
         //   movementDataModule.SetSpeed(movementDataModule.CurrentSpeed / 2);
            _timeLeft -= Time.deltaTime;
        }
        
        public override IEffect Clone() => new FreezeMovementEffect(_effectID);
        
    }
}