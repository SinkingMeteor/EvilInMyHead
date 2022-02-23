using Sheldier.Actors.Data;

namespace Sheldier.Gameplay.Effects
{
    public interface IMovementEffect : IEffect
    {
        void SetWrapper(IMovementEffect wrapper);
        MovementDataPackage GetMovementData(MovementDataPackage data);

        IMovementEffect Clone();
    }
}