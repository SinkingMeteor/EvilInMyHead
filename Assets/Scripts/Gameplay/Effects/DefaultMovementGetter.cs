using System;
using Sheldier.Actors.Data;

namespace Sheldier.Gameplay.Effects
{
    public class DefaultMovementGetter : IMovementEffect
    {
        public bool IsExpired => false;
        public EffectConfig Config => null;
        
        public void SetWrapper(IMovementEffect effect)
        {
            
        }

        public MovementDataPackage GetMovementData(MovementDataPackage data)
        {
            return data;
        }

        public void Setup()
        {
        }

        public void Tick()
        {
        }

        public IEffect Clone() => new DefaultMovementGetter();
    }
}