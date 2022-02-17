using Sheldier.Actors.Data;

namespace Sheldier.Gameplay.Effects
{
    public interface IMovementEffect : IEffect
    {
        void SetWrapper(IMovementEffect effect);
        MovementDataPackage GetMovementData(MovementDataPackage data);
    }
}